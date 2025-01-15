using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000164 RID: 356
	internal class TileLayerMapper
	{
		// Token: 0x06000E91 RID: 3729 RVA: 0x0003F4A8 File Offset: 0x0003D6A8
		internal TileLayerMapper(Map map, MapControl coreMap)
		{
			this.m_map = map;
			this.m_coreMap = coreMap;
			this.m_mapTileLayers = new Dictionary<string, MapTileLayer>();
			this.m_coreMap.mapCore.LoadTilesHandler = new LoadTilesHandler(this.LoadTilesHandler);
			this.m_coreMap.mapCore.SaveTilesHandler = new SaveTilesHandler(this.SaveTilesHandler);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003F50C File Offset: 0x0003D70C
		internal void AddLayer(MapTileLayer mapTileLayer)
		{
			this.m_mapTileLayers.Add(mapTileLayer.Name, mapTileLayer);
			this.m_coreMap.Layers[mapTileLayer.Name].TileSystem = this.GetTileSystem(mapTileLayer);
			this.m_coreMap.Layers[mapTileLayer.Name].UseSecureConnectionForTiles = this.GetUseSecureConnection(mapTileLayer);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003F570 File Offset: 0x0003D770
		private bool GetUseSecureConnection(MapTileLayer mapTileLayer)
		{
			ReportBoolProperty useSecureConnection = mapTileLayer.UseSecureConnection;
			if (useSecureConnection == null)
			{
				return false;
			}
			if (!useSecureConnection.IsExpression)
			{
				return useSecureConnection.Value;
			}
			return mapTileLayer.Instance.UseSecureConnection;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003F5A4 File Offset: 0x0003D7A4
		private TileSystem GetTileSystem(MapTileLayer mapTileLayer)
		{
			ReportEnumProperty<MapTileStyle> tileStyle = mapTileLayer.TileStyle;
			MapTileStyle mapTileStyle = MapTileStyle.Road;
			if (tileStyle != null)
			{
				if (!tileStyle.IsExpression)
				{
					mapTileStyle = tileStyle.Value;
				}
				else
				{
					mapTileStyle = mapTileLayer.Instance.TileStyle;
				}
			}
			if (mapTileStyle == MapTileStyle.Aerial)
			{
				return 1;
			}
			if (mapTileStyle != MapTileStyle.Hybrid)
			{
				return 3;
			}
			return 2;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003F5E9 File Offset: 0x0003D7E9
		private bool Embedded(MapTileLayer mapTileLayer)
		{
			return mapTileLayer.MapTiles != null;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003F5F4 File Offset: 0x0003D7F4
		private MapTileLayer GetLayer(string layerName)
		{
			MapTileLayer mapTileLayer;
			this.m_mapTileLayers.TryGetValue(layerName, out mapTileLayer);
			Global.Tracer.Assert(mapTileLayer != null, "null != tileLayer");
			return mapTileLayer;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003F624 File Offset: 0x0003D824
		private global::System.Drawing.Image[,] LoadTilesHandler(Layer layer, string[,] tileUrls)
		{
			global::System.Drawing.Image[,] array = null;
			int num = tileUrls.GetUpperBound(0) + 1;
			int num2 = tileUrls.GetUpperBound(1) + 1;
			MapTileLayer layer2 = this.GetLayer(layer.Name);
			try
			{
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						global::System.Drawing.Image image;
						if (this.Embedded(layer2))
						{
							image = this.GetEmbeddedTile(layer2, tileUrls[i, j]);
						}
						else
						{
							image = this.GetSnapshotTile(layer2, tileUrls[i, j]);
						}
						if (image == null)
						{
							this.DisposeTiles(array, num, num2);
							return null;
						}
						if (array == null)
						{
							array = new global::System.Drawing.Image[num, num2];
						}
						array[i, j] = image;
					}
				}
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				this.DisposeTiles(array, num, num2);
				return null;
			}
			this.m_success = array != null;
			return array;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003F708 File Offset: 0x0003D908
		private void DisposeTiles(global::System.Drawing.Image[,] tiles, int row, int col)
		{
			if (tiles != null)
			{
				for (int i = 0; i < row; i++)
				{
					for (int j = 0; j < col; j++)
					{
						if (tiles[i, j] != null)
						{
							tiles[i, j].Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003F748 File Offset: 0x0003D948
		private global::System.Drawing.Image GetSnapshotTile(MapTileLayer mapTileLayer, string url)
		{
			string text;
			Stream tileData = mapTileLayer.Instance.GetTileData(url, out text);
			if (tileData == null)
			{
				return null;
			}
			return global::System.Drawing.Image.FromStream(tileData);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003F770 File Offset: 0x0003D970
		private global::System.Drawing.Image GetEmbeddedTile(MapTileLayer mapTileLayer, string url)
		{
			foreach (MapTile mapTile in mapTileLayer.MapTiles)
			{
				if (mapTile.Name == url)
				{
					using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(mapTile.TileData)))
					{
						return global::System.Drawing.Image.FromStream(memoryStream);
					}
				}
			}
			return null;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003F7F8 File Offset: 0x0003D9F8
		private void SaveTilesHandler(Layer layer, string[,] tileUrls, global::System.Drawing.Image[,] tileImages)
		{
			MapTileLayer layer2 = this.GetLayer(layer.Name);
			if (this.Embedded(layer2) || this.m_success)
			{
				return;
			}
			int num = tileUrls.GetUpperBound(0) + 1;
			int num2 = tileUrls.GetUpperBound(1) + 1;
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					string text = tileUrls[i, j];
					global::System.Drawing.Image image = tileImages[i, j];
					using (MemoryStream memoryStream = new MemoryStream())
					{
						image.Save(memoryStream, ImageFormat.Png);
						string text2;
						if (layer2.Instance.GetTileData(text, out text2) == null)
						{
							layer2.Instance.SetTileData(text, memoryStream.ToArray(), null);
						}
					}
				}
			}
		}

		// Token: 0x04000704 RID: 1796
		private bool m_success;

		// Token: 0x04000705 RID: 1797
		private Map m_map;

		// Token: 0x04000706 RID: 1798
		private MapControl m_coreMap;

		// Token: 0x04000707 RID: 1799
		private Dictionary<string, MapTileLayer> m_mapTileLayers;
	}
}
