using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using Microsoft.InfoNav.Common;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200005D RID: 93
	internal sealed class ReportArchive
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000B37B File Offset: 0x0000957B
		private ReportArchive()
		{
			this._reportState = null;
			this._imageResourceMap = new ImageResourceMap();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B395 File Offset: 0x00009595
		internal ReportArchive(RdmReport report, ReportState reportState)
		{
			this._reportDefinition = report;
			this._reportState = reportState;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000B3AB File Offset: 0x000095AB
		public ReportState ReportState
		{
			get
			{
				return this._reportState;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000B3B3 File Offset: 0x000095B3
		internal RdmReport ReportDefinition
		{
			get
			{
				return this._reportDefinition;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000B3BB File Offset: 0x000095BB
		public ImageResourceMap ImageResourceMap
		{
			get
			{
				return this._imageResourceMap;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B3C3 File Offset: 0x000095C3
		public static ReportArchive Load(Stream stream)
		{
			ReportArchive reportArchive = new ReportArchive();
			reportArchive.InternalLoad(stream);
			return reportArchive;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B3D4 File Offset: 0x000095D4
		private void InternalLoad(Stream stream)
		{
			ZipArchive zipArchive = new ZipArchive(stream);
			this.ReadTopLevelRelationships(zipArchive);
			foreach (ZipArchiveEntry zipArchiveEntry in zipArchive.Entries.Where((ZipArchiveEntry entry) => entry.FullName != this._reportRelationshipsFile && entry.FullName != this._reportRelationshipsFile))
			{
				if (zipArchiveEntry.FullName == this._reportStateTarget)
				{
					this._reportState = ReportStateParser.Parse(new ReportParsingDiagnosticContext(), zipArchiveEntry.Open());
				}
				else if (zipArchiveEntry.FullName == this._reportDefinitonTarget)
				{
					ReportParser reportParser = new ReportParser();
					this._reportDefinition = reportParser.Parse(zipArchiveEntry.Open());
				}
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B490 File Offset: 0x00009690
		private void ReadRelationshipDocument(string fileName, ZipArchive zipArchive, Action<string, string, string> functionProcessEntry)
		{
			using (Stream stream = zipArchive.GetEntry(fileName).Open())
			{
				Contract.Check(stream != null, "Unable to get the following file from archive. file name:" + fileName);
				using (XmlReader xmlReader = XmlReader.Create(stream, XmlUtil.CreateSafeXmlReaderSettings()))
				{
					while (!xmlReader.EOF)
					{
						if (xmlReader.IsStartElement() && xmlReader.LocalName == "Relationship")
						{
							string attribute = xmlReader.GetAttribute("Id");
							string attribute2 = xmlReader.GetAttribute("Type");
							string attribute3 = xmlReader.GetAttribute("Target");
							functionProcessEntry(attribute, attribute2, attribute3);
						}
						xmlReader.Read();
					}
				}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B558 File Offset: 0x00009758
		private void ReadTopLevelRelationships(ZipArchive zipArchive)
		{
			this.ReadRelationshipDocument("_rels/.rels", zipArchive, delegate(string id, string type, string target)
			{
				if (type == ReportArchive.ReportDefinitionType)
				{
					this._reportDefinitonTarget = target;
				}
			});
			this.ReadReportLevelRelationships(this._reportDefinitonTarget, zipArchive);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B580 File Offset: 0x00009780
		private void ReadReportLevelRelationships(string reportDefinitionTarget, ZipArchive zipArchive)
		{
			int num = reportDefinitionTarget.LastIndexOf('/');
			string reportFolder;
			string text;
			if (num == -1)
			{
				reportFolder = string.Empty;
				text = reportDefinitionTarget;
			}
			else
			{
				reportFolder = reportDefinitionTarget.Substring(0, num + 1);
				text = reportDefinitionTarget.Substring(num + 1);
			}
			this._reportRelationshipsFile = reportFolder + "_rels/" + text + ".rels";
			this.ReadRelationshipDocument(this._reportRelationshipsFile, zipArchive, delegate(string id, string type, string target)
			{
				if (type == ReportArchive.ReportStateDocumentType)
				{
					this._reportStateTarget = reportFolder + target;
					return;
				}
				if (type == ReportArchive.ReportImageType)
				{
					ZipArchiveEntry entry = zipArchive.GetEntry(reportFolder + target);
					using (Stream stream = zipArchive.GetEntry(reportFolder + target).Open())
					{
						byte[] array = new byte[entry.Length];
						stream.Read(array, 0, Convert.ToInt32(entry.Length));
						this._imageResourceMap.Insert(id, array);
					}
				}
			});
		}

		// Token: 0x04000154 RID: 340
		private static readonly string ReportDefinitionType = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/reportdefinition";

		// Token: 0x04000155 RID: 341
		private static readonly string ReportStateDocumentType = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/reportstate";

		// Token: 0x04000156 RID: 342
		private static readonly string ReportImageType = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/image";

		// Token: 0x04000157 RID: 343
		private ReportState _reportState;

		// Token: 0x04000158 RID: 344
		private RdmReport _reportDefinition;

		// Token: 0x04000159 RID: 345
		private string _reportDefinitonTarget;

		// Token: 0x0400015A RID: 346
		private string _reportRelationshipsFile;

		// Token: 0x0400015B RID: 347
		private string _reportStateTarget;

		// Token: 0x0400015C RID: 348
		private ImageResourceMap _imageResourceMap;
	}
}
