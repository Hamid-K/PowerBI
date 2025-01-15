using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ProgressivePackaging;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000182 RID: 386
	internal sealed class GetExternalImagesAction : ProgressivePackageActionBase
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x000337D4 File Offset: 0x000319D4
		internal GetExternalImagesAction(IRenderEditSession session, Stream inputStream, Stream outputStream, IList<string> responseFlags, RSService service, string jobId)
			: base(outputStream, responseFlags, service)
		{
			RSTrace.CatalogTrace.Assert(session != null, "GetExternalImagesAction.ctor: session != null");
			RSTrace.CatalogTrace.Assert(inputStream != null, "GetExternalImagesAction.ctor: inputStream != null");
			this.m_session = session;
			this.m_inputStream = inputStream;
			this.m_jobId = jobId;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00033828 File Offset: 0x00031A28
		protected override bool InitializeAction()
		{
			return base.TryGetProgressivePackageReader(this.m_inputStream, out this.m_reader);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003383C File Offset: 0x00031A3C
		protected override void ExecuteAction()
		{
			ProgressiveCacheEntry progressiveCacheEntry;
			if (!base.EnsureValidSessionExists(this.m_session, out progressiveCacheEntry))
			{
				return;
			}
			RSTrace.CatalogTrace.Assert(this.m_reader != null, "ProgressivePackageReader is null");
			Stream stream = this.m_reader.ConsumeOptionalValue<Stream>("getExternalImagesRequest");
			if (stream == null)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "GetExternalImagesRequestStream is missing");
				base.MessageWriter.WriteMessage("serverErrorCode", "MissingGetExternalImagesRequest");
				return;
			}
			XmlReader xmlReader = XmlReader.Create(stream, GetExternalImagesAction.GetXmlReaderSettings());
			using (ImageRequestMessageReader imageRequestMessageReader = new ImageRequestMessageReader(xmlReader))
			{
				ExternalResourceAbortHelper externalResourceAbortHelper = new ExternalResourceAbortHelper();
				if (ProcessingContext.ThreadContext != null)
				{
					ProcessingContext.ThreadContext.AddAbortHelper(externalResourceAbortHelper);
				}
				try
				{
					this.ValidateXmlNamespace(xmlReader);
					progressiveCacheEntry.AddJob(this.m_jobId, false);
					ImageResponseMessageWriter imageResponseMessageWriter = new ImageResponseMessageWriter(base.MessageWriter);
					foreach (ImageRequestMessageElement imageRequestMessageElement in imageRequestMessageReader)
					{
						if (externalResourceAbortHelper.IsAborted)
						{
							break;
						}
						ImageResponseMessageElement imageResponseMessageElement = this.RetrieveImage(imageRequestMessageElement, externalResourceAbortHelper);
						imageResponseMessageWriter.WriteElement(imageResponseMessageElement);
					}
				}
				catch (XmlSchemaException ex)
				{
					throw new GetExternalImagesFailureException(ErrorStrings.GetExternalImagesInvalidSyntax, ErrorCode.rsInvalidXml, ex);
				}
				catch (XmlException ex2)
				{
					throw new GetExternalImagesFailureException(ErrorStrings.GetExternalImagesInvalidSyntax, ErrorCode.rsMalformedXml, ex2);
				}
				catch (FormatException ex3)
				{
					throw new GetExternalImagesFailureException(ErrorStrings.GetExternalImagesInvalidSyntax, ErrorCode.rsMalformedXml, ex3);
				}
				finally
				{
					progressiveCacheEntry.RemoveJob(this.m_jobId, false);
					if (ProcessingContext.ThreadContext != null)
					{
						ProcessingContext.ThreadContext.RemoveAbortHelper();
					}
				}
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000339EC File Offset: 0x00031BEC
		private ImageResponseMessageElement RetrieveImage(ImageRequestMessageElement requestElement, ExternalResourceAbortHelper abortHelper)
		{
			string imageUrl = requestElement.ImageUrl;
			string text = null;
			byte[] array = null;
			bool flag = false;
			string text2 = imageUrl;
			if (text == null)
			{
				try
				{
					using (MonitoredScope.NewFormat("{0}: Retrieving external resource at '{1}'", this.OperationName, text2))
					{
						string text3;
						bool flag2;
						this.m_service.ProcessingGetResource(null, imageUrl, true, abortHelper, out array, out text3, out flag2, out flag);
					}
				}
				catch (UriFormatException)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: Requested image URL '{1}' is in an invalid format", new object[] { this.OperationName, text2 });
					text = "ExternalImageInvalidUri";
				}
				catch (WebException ex)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: WebException occurred while retrieving image at URL '{1}': {2}", new object[] { this.OperationName, text2, ex.Message });
					if (ex.Response is HttpWebResponse)
					{
						text = "ExternalImageHttpError";
					}
					else
					{
						text = "ExternalImageNetworkError";
					}
				}
				catch (Exception ex2)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: Unexpected error occurred while retreiving image at URL '{1}': {2}", new object[] { this.OperationName, text2, ex2.Message });
					text = "ExternalImageUnexpectedError";
				}
			}
			if (flag)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "{0}: Requested image '{1}' exceeds maximum allowed resource size.", new object[] { this.OperationName, text2 });
				text = "ExternalImageInvalidContent";
			}
			if (text == null)
			{
				try
				{
					RVUnit rvunit = new RVUnit(requestElement.ImageWidth, CultureInfo.InvariantCulture);
					RVUnit rvunit2 = new RVUnit(requestElement.ImageHeight, CultureInfo.InvariantCulture);
					using (MonitoredScope.NewFormat("{0}: Scaling external image '{1}", this.OperationName, text2))
					{
						array = ImageUtility.ScaleImage(array, rvunit, rvunit2);
					}
					if (array == null)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: Contents of image '{1}' could not be processed.", new object[] { this.OperationName, text2 });
						text = "ExternalImageInvalidContent";
					}
				}
				catch (Exception ex3)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: Error occurred while processing image '{1}': {2}", new object[] { this.OperationName, text2, ex3.Message });
					text = "ExternalImageInvalidContent";
					array = null;
				}
			}
			return new ImageResponseMessageElement(imageUrl, requestElement.ImageWidth, requestElement.ImageHeight, array, text);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00033C50 File Offset: 0x00031E50
		private static XmlReaderSettings GetXmlReaderSettings()
		{
			if (GetExternalImagesAction.m_readerSettings == null)
			{
				XmlReaderSettings xmlReaderSettings = ProgressiveXmlUtil.CreateXmlReaderSettings("Microsoft.ReportingServices.ProgressiveReport.GetExternalImages.xsd");
				Interlocked.CompareExchange<XmlReaderSettings>(ref GetExternalImagesAction.m_readerSettings, xmlReaderSettings, null);
			}
			return GetExternalImagesAction.m_readerSettings;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00033C84 File Offset: 0x00031E84
		private void ValidateXmlNamespace(XmlReader reader)
		{
			GetExternalImagesAction.GetExternalImagesVersion? getExternalImagesVersion = null;
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element && "ClientRequest".Equals(reader.Name, StringComparison.Ordinal))
				{
					if (reader.MoveToAttribute("xmlns"))
					{
						getExternalImagesVersion = new GetExternalImagesAction.GetExternalImagesVersion?(this.GetVersionFromNamespace(reader.Value));
						break;
					}
					break;
				}
			}
			if (getExternalImagesVersion == null)
			{
				throw new GetExternalImagesFailureException(ErrorStrings.GetExternalImagesInvalidNamespace, ErrorCode.rsInvalidXml);
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00033CF6 File Offset: 0x00031EF6
		private GetExternalImagesAction.GetExternalImagesVersion GetVersionFromNamespace(string requestNamespace)
		{
			if ("http://schemas.microsoft.com/sqlserver/reporting/2011/01/getexternalimages".Equals(requestNamespace, StringComparison.Ordinal))
			{
				return GetExternalImagesAction.GetExternalImagesVersion.InitialVersion;
			}
			throw new GetExternalImagesFailureException(ErrorStrings.GetExternalImagesInvalidNamespace, ErrorCode.rsInvalidXml);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void CleanupForException()
		{
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00033D13 File Offset: 0x00031F13
		protected override void FinalCleanup(ErrorCode status)
		{
			if (this.m_reader != null)
			{
				this.m_reader.Dispose();
				this.m_reader = null;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00033D2F File Offset: 0x00031F2F
		protected override string OperationName
		{
			get
			{
				return "GetExternalImages";
			}
		}

		// Token: 0x040005CB RID: 1483
		private const string XsdResourceName = "Microsoft.ReportingServices.ProgressiveReport.GetExternalImages.xsd";

		// Token: 0x040005CC RID: 1484
		private static XmlReaderSettings m_readerSettings;

		// Token: 0x040005CD RID: 1485
		private readonly IRenderEditSession m_session;

		// Token: 0x040005CE RID: 1486
		private readonly Stream m_inputStream;

		// Token: 0x040005CF RID: 1487
		private ProgressivePackageReader m_reader;

		// Token: 0x040005D0 RID: 1488
		private readonly string m_jobId;

		// Token: 0x02000478 RID: 1144
		private enum GetExternalImagesVersion
		{
			// Token: 0x04000FEE RID: 4078
			InitialVersion
		}
	}
}
