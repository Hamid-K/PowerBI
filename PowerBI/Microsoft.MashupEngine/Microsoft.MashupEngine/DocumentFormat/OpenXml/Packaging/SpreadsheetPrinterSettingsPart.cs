using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A2 RID: 8610
	internal class SpreadsheetPrinterSettingsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DA52 RID: 55890 RVA: 0x002AD898 File Offset: 0x002ABA98
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (SpreadsheetPrinterSettingsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SpreadsheetPrinterSettingsPart._partConstraint = dictionary;
			}
			return SpreadsheetPrinterSettingsPart._partConstraint;
		}

		// Token: 0x0600DA53 RID: 55891 RVA: 0x002AD8C0 File Offset: 0x002ABAC0
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (SpreadsheetPrinterSettingsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				SpreadsheetPrinterSettingsPart._dataPartReferenceConstraint = dictionary;
			}
			return SpreadsheetPrinterSettingsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA54 RID: 55892 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal SpreadsheetPrinterSettingsPart()
		{
		}

		// Token: 0x170036A2 RID: 13986
		// (get) Token: 0x0600DA55 RID: 55893 RVA: 0x002AD8E5 File Offset: 0x002ABAE5
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings";
			}
		}

		// Token: 0x170036A3 RID: 13987
		// (get) Token: 0x0600DA56 RID: 55894 RVA: 0x002AD8EC File Offset: 0x002ABAEC
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.printerSettings";
			}
		}

		// Token: 0x170036A4 RID: 13988
		// (get) Token: 0x0600DA57 RID: 55895 RVA: 0x002AD8F3 File Offset: 0x002ABAF3
		internal sealed override string TargetPath
		{
			get
			{
				return "../printerSettings";
			}
		}

		// Token: 0x170036A5 RID: 13989
		// (get) Token: 0x0600DA58 RID: 55896 RVA: 0x002AD8FA File Offset: 0x002ABAFA
		internal sealed override string TargetName
		{
			get
			{
				return "printerSettings";
			}
		}

		// Token: 0x170036A6 RID: 13990
		// (get) Token: 0x0600DA59 RID: 55897 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x170036A7 RID: 13991
		// (get) Token: 0x0600DA5A RID: 55898 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006BD1 RID: 27601
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings";

		// Token: 0x04006BD2 RID: 27602
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.printerSettings";

		// Token: 0x04006BD3 RID: 27603
		internal const string TargetPathConstant = "../printerSettings";

		// Token: 0x04006BD4 RID: 27604
		internal const string TargetNameConstant = "printerSettings";

		// Token: 0x04006BD5 RID: 27605
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006BD6 RID: 27606
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BD7 RID: 27607
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
