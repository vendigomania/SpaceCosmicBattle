﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace MatchThreeMachine
{
	public sealed class BoardLogic : MonoBehaviour
	{
		[SerializeField] private TileTypeDataAsset[] tileTypes;

		[SerializeField] private TilesRow[] rows;

		[SerializeField] private float tweenDuration;

		[SerializeField] private Transform swappingOverlay;

		[SerializeField] private bool ensureNoStartingMatches;

		private readonly List<TileVisualizator> _selection = new List<TileVisualizator>();

		private bool _isSwapping;
		private bool _isMatching;
		private bool _isShuffling;

		public event Action<TileTypeDataAsset, int> OnMatch;

		private TileData[,] Matrix
		{
			get
			{
				var width = rows.Max(row => row.tiles.Length);
				var height = rows.Length;

				var data = new TileData[width, height];

				for (var y = 0; y < height; y++)
					for (var x = 0; x < width; x++)
						data[x, y] = GetTile(x, y).Data;

				return data;
			}
		}

		private void Start()
		{
			for (var y = 0; y < rows.Length; y++)
			{
				for (var x = 0; x < rows.Max(row => row.tiles.Length); x++)
				{
					var tile = GetTile(x, y);

					tile.xPosition = x;
					tile.yPosition = y;

					tile.Type = tileTypes[Random.Range(0, tileTypes.Length)];

					tile.button.onClick.AddListener(() => Select(tile));
				}
			}

			if (ensureNoStartingMatches) StartCoroutine(EnsureNoStartingMatches());

			OnMatch += (type, count) => Debug.Log($"Matched {count}x {type.name}.");
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				var bestMove = TileDataMatrixUtility.FindBestMove(Matrix);

				if (bestMove != null)
				{
					Select(GetTile(bestMove.startX, bestMove.startY));
					Select(GetTile(bestMove.endX, bestMove.endY));
				}
			}
		}

		private IEnumerator EnsureNoStartingMatches()
		{
			var wait = new WaitForEndOfFrame();

			while (TileDataMatrixUtility.FindBestMatch(Matrix) != null)
			{
				Shuffle();

				yield return wait;
			}
		}

		private TileVisualizator GetTile(int x, int y) => rows[y].tiles[x];

		private TileVisualizator[] GetTiles(IList<TileData> tileData)
		{
			var length = tileData.Count;

			var tiles = new TileVisualizator[length];

			for (var i = 0; i < length; i++) tiles[i] = GetTile(tileData[i].X, tileData[i].Y);

			return tiles;
		}

		private async void Select(TileVisualizator tile)
		{
			if (_isSwapping || _isMatching || _isShuffling) return;

			if (!_selection.Contains(tile))
			{
				if (_selection.Count > 0)
				{
					if (Math.Abs(tile.xPosition - _selection[0].xPosition) == 1 && Math.Abs(tile.yPosition - _selection[0].yPosition) == 0
					    || Math.Abs(tile.yPosition - _selection[0].yPosition) == 1 && Math.Abs(tile.xPosition - _selection[0].xPosition) == 0)
                    {
	                    _selection.Add(tile);
                    }

					else
                    {
						_selection.Clear();
                    }
				}
				else
				{
					_selection.Add(tile);
					
				}
			}

			AudioManager.Click();

			if (_selection.Count < 2) return;

			await SwapAsync(_selection[0], _selection[1]);

			if (!await TryMatchAsync()) await SwapAsync(_selection[0], _selection[1]);

			var matrix = Matrix;

			while (TileDataMatrixUtility.FindBestMove(matrix) == null || TileDataMatrixUtility.FindBestMatch(matrix) != null)
			{
				Shuffle();

				matrix = Matrix;
			}

			_selection.Clear();
		}

		private async Task SwapAsync(TileVisualizator tile1, TileVisualizator tile2)
		{
			_isSwapping = true;

			var icon1 = tile1.icon;
			var icon2 = tile2.icon;

			var icon1Transform = icon1.transform;
			var icon2Transform = icon2.transform;

			icon1Transform.SetParent(swappingOverlay);
			icon2Transform.SetParent(swappingOverlay);

			icon1Transform.SetAsLastSibling();
			icon2Transform.SetAsLastSibling();

			var sequence = DOTween.Sequence();

			sequence.Join(icon1Transform.DOMove(icon2Transform.position, tweenDuration).SetEase(Ease.OutBack))
			        .Join(icon2Transform.DOMove(icon1Transform.position, tweenDuration).SetEase(Ease.OutBack));

			await sequence.Play()
			              .AsyncWaitForCompletion();

			icon1Transform.SetParent(tile2.transform);
			icon2Transform.SetParent(tile1.transform);

			tile1.icon = icon2;
			tile2.icon = icon1;

			var tile1Item = tile1.Type;

			tile1.Type = tile2.Type;

			tile2.Type = tile1Item;

			_isSwapping = false;
		}

		private async Task<bool> TryMatchAsync()
		{
			var didMatch = false;

			_isMatching = true;

			var match = TileDataMatrixUtility.FindBestMatch(Matrix);

			while (match != null)
			{
				didMatch = true;

				var tiles = GetTiles(match.MatchedTiles);

				var deflateSequence = DOTween.Sequence();

				foreach (var tile in tiles) deflateSequence.Join(tile.icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));

				await deflateSequence.Play()
				                     .AsyncWaitForCompletion();

				var inflateSequence = DOTween.Sequence();

				foreach (var tile in tiles)
				{
					tile.Type = tileTypes[Random.Range(0, tileTypes.Length)];

					inflateSequence.Join(tile.icon.transform.DOScale(Vector3.one, tweenDuration).SetEase(Ease.OutBack));
				}

				await inflateSequence.Play()
				                     .AsyncWaitForCompletion();

				OnMatch?.Invoke(Array.Find(tileTypes, tileType => tileType.id == match.TypeId), match.MatchedTiles.Length);

				match = TileDataMatrixUtility.FindBestMatch(Matrix);
			}

			_isMatching = false;

			return didMatch;
		}

		private void Shuffle()
		{
			_isShuffling = true;

			foreach (var row in rows)
				foreach (var tile in row.tiles)
					tile.Type = tileTypes[Random.Range(0, tileTypes.Length)];

			_isShuffling = false;
		}
	}
}
