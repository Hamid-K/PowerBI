using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020021AA RID: 8618
	internal class VbaProjectPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DAA0 RID: 55968 RVA: 0x002ADDB8 File Offset: 0x002ABFB8
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (VbaProjectPart._partConstraint == null)
			{
				VbaProjectPart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.microsoft.com/office/2006/relationships/wordVbaData",
					new PartConstraintRule("VbaDataPart", "application/vnd.ms-word.vbaData+xml", false, false, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return VbaProjectPart._partConstraint;
		}

		// Token: 0x0600DAA1 RID: 55969 RVA: 0x002ADDFC File Offset: 0x002ABFFC
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (VbaProjectPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				VbaProjectPart._dataPartReferenceConstraint = dictionary;
			}
			return VbaProjectPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DAA2 RID: 55970 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal VbaProjectPart()
		{
		}

		// Token: 0x0600DAA3 RID: 55971 RVA: 0x002ADE24 File Offset: 0x002AC024
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.microsoft.com/office/2006/relationships/wordVbaData")
			{
				return new VbaDataPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x170036CE RID: 14030
		// (get) Token: 0x0600DAA4 RID: 55972 RVA: 0x002ADE67 File Offset: 0x002AC067
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.microsoft.com/office/2006/relationships/vbaProject";
			}
		}

		// Token: 0x170036CF RID: 14031
		// (get) Token: 0x0600DAA5 RID: 55973 RVA: 0x002ADE6E File Offset: 0x002AC06E
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.ms-office.vbaProject";
			}
		}

		// Token: 0x170036D0 RID: 14032
		// (get) Token: 0x0600DAA6 RID: 55974 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170036D1 RID: 14033
		// (get) Token: 0x0600DAA7 RID: 55975 RVA: 0x002ADE75 File Offset: 0x002AC075
		internal sealed override string TargetName
		{
			get
			{
				return "vbaProject";
			}
		}

		// Token: 0x170036D2 RID: 14034
		// (get) Token: 0x0600DAA8 RID: 55976 RVA: 0x002ADE7C File Offset: 0x002AC07C
		public VbaDataPart VbaDataPart
		{
			get
			{
				return base.GetSubPartOfType<VbaDataPart>();
			}
		}

		// Token: 0x170036D3 RID: 14035
		// (get) Token: 0x0600DAA9 RID: 55977 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04006BFF RID: 27647
		internal const string RelationshipTypeConstant = "http://schemas.microsoft.com/office/2006/relationships/vbaProject";

		// Token: 0x04006C00 RID: 27648
		internal const string ContentTypeConstant = "application/vnd.ms-office.vbaProject";

		// Token: 0x04006C01 RID: 27649
		internal const string TargetPathConstant = ".";

		// Token: 0x04006C02 RID: 27650
		internal const string TargetNameConstant = "vbaProject";

		// Token: 0x04006C03 RID: 27651
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006C04 RID: 27652
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
