using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Reflection;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000171 RID: 369
	internal static class PowerPointExport
	{
		// Token: 0x06000D9F RID: 3487 RVA: 0x00031780 File Offset: 0x0002F980
		public static void Export(Stream rdlxStream, string appUrl, string reportServer, string rdlxPath, Dictionary<string, Stream> images, CreateAndRegisterStream createAndRegisterStreamCallback)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(rdlxPath);
			Stream stream = createAndRegisterStreamCallback(fileNameWithoutExtension, "pptx", null, "application/vnd.openxmlformats-officedocument.presentationml.presentation", true, StreamOper.CreateAndRegister);
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Microsoft.ReportingServices.Library.PowerPointExport.CrescentTemplate.pptx"))
			{
				StreamSupport.CopyStreamUsingBuffer(manifestResourceStream, stream, 4096);
			}
			PersistStorage persistStorage = null;
			Dictionary<string, Stream> dictionary = null;
			ReportArchive reportArchive = null;
			try
			{
				reportArchive = ReportArchive.Load(rdlxStream);
				RdlxReport rdlxReport = RdlxReport.Load(reportArchive.GetReportDefinitionStream());
				dictionary = reportArchive.GetSectionPreviewLocationsMap();
				SilverlightProperties silverlightProperties = new SilverlightProperties(appUrl);
				silverlightProperties.StringUICulture = CultureInfo.CurrentUICulture.Name;
				silverlightProperties.StringCulture = CultureInfo.CurrentCulture.Name;
				int num = 1;
				using (Presentation presentation = new Presentation(stream))
				{
					List<Uri> list = new List<Uri>();
					foreach (ReportSection reportSection in rdlxReport.Sections)
					{
						persistStorage = new PersistStorage();
						Stream stream2 = null;
						if (images != null)
						{
							images.TryGetValue(reportSection.Name, out stream2);
						}
						if (stream2 == null && reportSection.PreviewId != null)
						{
							dictionary.TryGetValue(reportSection.PreviewId, out stream2);
						}
						silverlightProperties.StringInitParams = string.Format(CultureInfo.InvariantCulture, "ItemPath={0},ReportServerUri={1},ViewMode={2},ReportSection={3},AllowSectionNavigation=False,ApplicationHost=PowerPoint,Fit=True,PreviewBar=False,BackgroundColor=White,Border=True,AllowEditViewMode=False,AllowFullScreenViewMode=False,Trace_HostApplication=Powerpoint", new object[]
						{
							rdlxPath,
							reportServer,
							(stream2 == null) ? "Presentation" : "CachedPreview",
							reportSection.Name
						});
						if (stream2 == null)
						{
							stream2 = executingAssembly.GetManifestResourceStream("Microsoft.ReportingServices.Library.PowerPointExport.NoPreview.png");
						}
						silverlightProperties.LoadImage(stream2);
						using (Stream stream3 = new SegmentedMemoryStream((int)stream2.Length + 256))
						{
							silverlightProperties.Serialize(stream3);
							persistStorage.SetContent(stream3);
						}
						if (num == 1)
						{
							PackagePart packagePart;
							PackagePart packagePart2;
							presentation.UpdateSlide(num, persistStorage.GetData(), stream2, out packagePart, out packagePart2);
						}
						else
						{
							list.Add(presentation.AddSlide(num, persistStorage.GetData(), stream2));
						}
						num++;
					}
					presentation.AddSlidesToPresentation(list);
				}
			}
			finally
			{
				if (persistStorage != null)
				{
					persistStorage.Close();
				}
				if (reportArchive != null)
				{
					reportArchive.Close();
				}
			}
		}

		// Token: 0x0400059D RID: 1437
		private const string FileExtension = "pptx";

		// Token: 0x0400059E RID: 1438
		public const string MimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
	}
}
