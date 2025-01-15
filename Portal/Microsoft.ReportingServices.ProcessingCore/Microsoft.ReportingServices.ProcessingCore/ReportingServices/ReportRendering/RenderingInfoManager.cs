using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000012 RID: 18
	internal sealed class RenderingInfoManager
	{
		// Token: 0x06000325 RID: 805 RVA: 0x00007B4B File Offset: 0x00005D4B
		internal RenderingInfoManager(string rendererID, ReportProcessing.GetReportChunk getChunkCallback, bool retrieveRenderingInfo)
		{
			this.m_chunkName = "RenderingInfo_" + rendererID;
			if (retrieveRenderingInfo)
			{
				this.m_renderingInfoRoot = this.Deserialize(getChunkCallback);
				return;
			}
			this.m_renderingInfoRoot = null;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00007B7C File Offset: 0x00005D7C
		internal Hashtable RenderingInfo
		{
			get
			{
				return this.RenderingInfoRoot.RenderingInfo;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00007B89 File Offset: 0x00005D89
		internal Hashtable SharedRenderingInfo
		{
			get
			{
				return this.RenderingInfoRoot.SharedRenderingInfo;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00007B96 File Offset: 0x00005D96
		internal Hashtable PageSectionRenderingInfo
		{
			get
			{
				return this.RenderingInfoRoot.PageSectionRenderingInfo;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00007BA3 File Offset: 0x00005DA3
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00007BB0 File Offset: 0x00005DB0
		internal PaginationInfo PaginationInfo
		{
			get
			{
				return this.RenderingInfoRoot.PaginationInfo;
			}
			set
			{
				this.RenderingInfoRoot.PaginationInfo = value;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00007BBE File Offset: 0x00005DBE
		private RenderingInfoRoot RenderingInfoRoot
		{
			get
			{
				if (this.m_renderingInfoRoot == null)
				{
					this.m_renderingInfoRoot = new RenderingInfoRoot();
				}
				return this.m_renderingInfoRoot;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00007BD9 File Offset: 0x00005DD9
		internal void Save(ReportProcessing.CreateReportChunk createChunkCallback)
		{
			if (this.m_renderingInfoRoot == null)
			{
				return;
			}
			this.Serialize(this.m_renderingInfoRoot, createChunkCallback);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00007BF4 File Offset: 0x00005DF4
		private RenderingInfoRoot Deserialize(ReportProcessing.GetReportChunk getChunkCallback)
		{
			Stream stream = null;
			RenderingInfoRoot renderingInfoRoot;
			try
			{
				string text;
				stream = getChunkCallback(this.m_chunkName, ReportProcessing.ReportChunkTypes.Other, out text);
				RenderingInfoRoot result = null;
				if (stream != null)
				{
					BinaryFormatter bFormatter = new BinaryFormatter();
					RevertImpersonationContext.Run(delegate
					{
						result = (RenderingInfoRoot)bFormatter.Deserialize(stream);
					});
				}
				renderingInfoRoot = result;
			}
			catch (SerializationException)
			{
				renderingInfoRoot = null;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			return renderingInfoRoot;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00007C90 File Offset: 0x00005E90
		private void Serialize(RenderingInfoRoot renderingInfoRoot, ReportProcessing.CreateReportChunk createChunkCallback)
		{
			Stream stream = null;
			try
			{
				stream = createChunkCallback(this.m_chunkName, ReportProcessing.ReportChunkTypes.Other, null);
				if (stream != null)
				{
					new BinaryFormatter().Serialize(stream, renderingInfoRoot);
				}
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x04000032 RID: 50
		private const string RenderingInfoChunkPrefix = "RenderingInfo_";

		// Token: 0x04000033 RID: 51
		private RenderingInfoRoot m_renderingInfoRoot;

		// Token: 0x04000034 RID: 52
		private string m_chunkName;
	}
}
