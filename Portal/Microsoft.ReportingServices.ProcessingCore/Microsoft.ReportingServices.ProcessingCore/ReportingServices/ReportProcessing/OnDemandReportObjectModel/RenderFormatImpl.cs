using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B3 RID: 1971
	internal sealed class RenderFormatImpl : RenderFormatImplBase
	{
		// Token: 0x06006FF7 RID: 28663 RVA: 0x001D29F0 File Offset: 0x001D0BF0
		internal RenderFormatImpl(OnDemandProcessingContext odpContext)
		{
			this.m_odpContext = odpContext;
			this.m_emptyDeviceInfo = new ReadOnlyNameValueCollection(new NameValueCollection(0));
			if (this.m_odpContext.TopLevelContext.ReportContext.RSRequestParameters != null)
			{
				NameValueCollection renderingParameters = this.m_odpContext.TopLevelContext.ReportContext.RSRequestParameters.RenderingParameters;
				if (renderingParameters != null)
				{
					this.m_deviceInfo = new ReadOnlyNameValueCollection(renderingParameters);
				}
			}
			this.m_format = RenderFormatImpl.NormalizeRenderFormat(this.m_odpContext.TopLevelContext.ReportContext, out this.m_isInteractiveFormat);
			if (this.m_deviceInfo == null)
			{
				this.m_deviceInfo = this.m_emptyDeviceInfo;
			}
		}

		// Token: 0x06006FF8 RID: 28664 RVA: 0x001D2A94 File Offset: 0x001D0C94
		internal static string NormalizeRenderFormat(ICatalogItemContext reportContext, out bool isInteractiveFormat)
		{
			string text = null;
			if (reportContext.RSRequestParameters != null)
			{
				text = reportContext.RSRequestParameters.FormatParamValue;
			}
			if (text == null)
			{
				text = "RPL";
				isInteractiveFormat = true;
			}
			else if (RenderFormatImpl.IsRenderFormat(text, "RPL") || RenderFormatImpl.IsRenderFormat(text, "RGDI") || RenderFormatImpl.IsRenderFormat(text, "HTML4.0") || RenderFormatImpl.IsRenderFormat(text, "HTML5") || RenderFormatImpl.IsRenderFormat(text, "MHTML") || RenderFormatImpl.IsRenderFormat(text, "JSONRPL"))
			{
				isInteractiveFormat = true;
			}
			else
			{
				isInteractiveFormat = false;
			}
			return text;
		}

		// Token: 0x17002624 RID: 9764
		// (get) Token: 0x06006FF9 RID: 28665 RVA: 0x001D2B1C File Offset: 0x001D0D1C
		internal override string Name
		{
			get
			{
				this.SetRenderFormatUsed();
				if (this.IsRenderFormatAccessEnabled())
				{
					return this.m_format;
				}
				return null;
			}
		}

		// Token: 0x17002625 RID: 9765
		// (get) Token: 0x06006FFA RID: 28666 RVA: 0x001D2B34 File Offset: 0x001D0D34
		internal override bool IsInteractive
		{
			get
			{
				if (this.m_odpContext.IsTablixProcessingMode)
				{
					return false;
				}
				this.SetRenderFormatUsed();
				return this.IsInteractiveFormat();
			}
		}

		// Token: 0x17002626 RID: 9766
		// (get) Token: 0x06006FFB RID: 28667 RVA: 0x001D2B51 File Offset: 0x001D0D51
		internal override ReadOnlyNameValueCollection DeviceInfo
		{
			get
			{
				this.SetRenderFormatUsed();
				if (this.IsRenderFormatAccessEnabled())
				{
					return this.m_deviceInfo;
				}
				return this.m_emptyDeviceInfo;
			}
		}

		// Token: 0x06006FFC RID: 28668 RVA: 0x001D2B6E File Offset: 0x001D0D6E
		private bool IsRenderFormatAccessEnabled()
		{
			return !this.m_odpContext.IsTablixProcessingMode && (!this.IsInteractiveFormat() || this.m_odpContext.IsUnrestrictedRenderFormatReferenceMode);
		}

		// Token: 0x06006FFD RID: 28669 RVA: 0x001D2B97 File Offset: 0x001D0D97
		private bool IsInteractiveFormat()
		{
			return this.m_isInteractiveFormat;
		}

		// Token: 0x06006FFE RID: 28670 RVA: 0x001D2B9F File Offset: 0x001D0D9F
		internal static bool IsRenderFormat(string format, string targetFormat)
		{
			return ReportProcessing.CompareWithInvariantCulture(format, targetFormat, true) == 0;
		}

		// Token: 0x06006FFF RID: 28671 RVA: 0x001D2BAC File Offset: 0x001D0DAC
		private void RegisterWarning(string propertyName)
		{
			if (this.m_odpContext.ReportRuntime.RuntimeErrorContext == null)
			{
				return;
			}
			this.m_odpContext.ReportRuntime.RuntimeErrorContext.Register(ProcessingErrorCode.rsInvalidRenderFormatUsage, Severity.Warning, this.m_odpContext.ReportRuntime.ObjectType, this.m_odpContext.ReportRuntime.ObjectName, this.m_odpContext.ReportRuntime.PropertyName, Array.Empty<string>());
		}

		// Token: 0x06007000 RID: 28672 RVA: 0x001D2C1D File Offset: 0x001D0E1D
		private void SetRenderFormatUsed()
		{
			if (!this.m_odpContext.IsTablixProcessingMode)
			{
				this.m_odpContext.HasRenderFormatDependencyInDocumentMap = true;
			}
		}

		// Token: 0x040039D5 RID: 14805
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x040039D6 RID: 14806
		private string m_format;

		// Token: 0x040039D7 RID: 14807
		private bool m_isInteractiveFormat;

		// Token: 0x040039D8 RID: 14808
		private ReadOnlyNameValueCollection m_deviceInfo;

		// Token: 0x040039D9 RID: 14809
		private ReadOnlyNameValueCollection m_emptyDeviceInfo;

		// Token: 0x040039DA RID: 14810
		internal const string InteractivityRenderFormat = "RPL";
	}
}
