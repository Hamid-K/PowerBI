using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021A3 RID: 8611
	internal class WordprocessingPrinterSettingsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DA5B RID: 55899 RVA: 0x002AD904 File Offset: 0x002ABB04
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (WordprocessingPrinterSettingsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WordprocessingPrinterSettingsPart._partConstraint = dictionary;
			}
			return WordprocessingPrinterSettingsPart._partConstraint;
		}

		// Token: 0x0600DA5C RID: 55900 RVA: 0x002AD92C File Offset: 0x002ABB2C
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (WordprocessingPrinterSettingsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				WordprocessingPrinterSettingsPart._dataPartReferenceConstraint = dictionary;
			}
			return WordprocessingPrinterSettingsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA5D RID: 55901 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal WordprocessingPrinterSettingsPart()
		{
		}

		// Token: 0x170036A8 RID: 13992
		// (get) Token: 0x0600DA5E RID: 55902 RVA: 0x002AD8E5 File Offset: 0x002ABAE5
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings";
			}
		}

		// Token: 0x170036A9 RID: 13993
		// (get) Token: 0x0600DA5F RID: 55903 RVA: 0x002AD951 File Offset: 0x002ABB51
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.printerSettings";
			}
		}

		// Token: 0x170036AA RID: 13994
		// (get) Token: 0x0600DA60 RID: 55904 RVA: 0x002AD8F3 File Offset: 0x002ABAF3
		internal sealed override string TargetPath
		{
			get
			{
				return "../printerSettings";
			}
		}

		// Token: 0x170036AB RID: 13995
		// (get) Token: 0x0600DA61 RID: 55905 RVA: 0x002AD8FA File Offset: 0x002ABAFA
		internal sealed override string TargetName
		{
			get
			{
				return "printerSettings";
			}
		}

		// Token: 0x170036AC RID: 13996
		// (get) Token: 0x0600DA62 RID: 55906 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x170036AD RID: 13997
		// (get) Token: 0x0600DA63 RID: 55907 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006BD8 RID: 27608
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/printerSettings";

		// Token: 0x04006BD9 RID: 27609
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.wordprocessingml.printerSettings";

		// Token: 0x04006BDA RID: 27610
		internal const string TargetPathConstant = "../printerSettings";

		// Token: 0x04006BDB RID: 27611
		internal const string TargetNameConstant = "printerSettings";

		// Token: 0x04006BDC RID: 27612
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006BDD RID: 27613
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BDE RID: 27614
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
