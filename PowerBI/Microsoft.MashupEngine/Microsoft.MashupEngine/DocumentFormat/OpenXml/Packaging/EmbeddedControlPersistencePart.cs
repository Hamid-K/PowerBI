using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200219E RID: 8606
	internal class EmbeddedControlPersistencePart : OpenXmlPart
	{
		// Token: 0x0600DA2C RID: 55852 RVA: 0x002AD614 File Offset: 0x002AB814
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (EmbeddedControlPersistencePart._partConstraint == null)
			{
				EmbeddedControlPersistencePart._partConstraint = new Dictionary<string, PartConstraintRule> { 
				{
					"http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary",
					new PartConstraintRule("EmbeddedControlPersistenceBinaryDataPart", null, false, true, FileFormatVersions.Office2007 | FileFormatVersions.Office2010)
				} };
			}
			return EmbeddedControlPersistencePart._partConstraint;
		}

		// Token: 0x0600DA2D RID: 55853 RVA: 0x002AD654 File Offset: 0x002AB854
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (EmbeddedControlPersistencePart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				EmbeddedControlPersistencePart._dataPartReferenceConstraint = dictionary;
			}
			return EmbeddedControlPersistencePart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA2E RID: 55854 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal EmbeddedControlPersistencePart()
		{
		}

		// Token: 0x0600DA2F RID: 55855 RVA: 0x002AD67C File Offset: 0x002AB87C
		internal sealed override OpenXmlPart CreatePartCore(string relationshipType)
		{
			this.ThrowIfObjectDisposed();
			if (relationshipType == null)
			{
				throw new ArgumentNullException("relationshipType");
			}
			if (relationshipType != null && relationshipType == "http://schemas.microsoft.com/office/2006/relationships/activeXControlBinary")
			{
				return new EmbeddedControlPersistenceBinaryDataPart();
			}
			throw new ArgumentOutOfRangeException("relationshipType");
		}

		// Token: 0x0600DA30 RID: 55856 RVA: 0x002AD6C0 File Offset: 0x002AB8C0
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			base.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600DA31 RID: 55857 RVA: 0x002AD6DC File Offset: 0x002AB8DC
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType);
		}

		// Token: 0x0600DA32 RID: 55858 RVA: 0x002AD710 File Offset: 0x002AB910
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(string contentType, string id)
		{
			EmbeddedControlPersistenceBinaryDataPart embeddedControlPersistenceBinaryDataPart = new EmbeddedControlPersistenceBinaryDataPart();
			this.InitPart<EmbeddedControlPersistenceBinaryDataPart>(embeddedControlPersistenceBinaryDataPart, contentType, id);
			return embeddedControlPersistenceBinaryDataPart;
		}

		// Token: 0x0600DA33 RID: 55859 RVA: 0x002AD730 File Offset: 0x002AB930
		public EmbeddedControlPersistenceBinaryDataPart AddEmbeddedControlPersistenceBinaryDataPart(EmbeddedControlPersistenceBinaryDataPartType partType, string id)
		{
			string contentType = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetContentType(partType);
			string targetExtension = EmbeddedControlPersistenceBinaryDataPartTypeInfo.GetTargetExtension(partType);
			base.OpenXmlPackage.PartExtensionProvider.MakeSurePartExtensionExist(contentType, targetExtension);
			return this.AddEmbeddedControlPersistenceBinaryDataPart(contentType, id);
		}

		// Token: 0x1700368D RID: 13965
		// (get) Token: 0x0600DA34 RID: 55860 RVA: 0x002AD765 File Offset: 0x002AB965
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control";
			}
		}

		// Token: 0x1700368E RID: 13966
		// (get) Token: 0x0600DA35 RID: 55861 RVA: 0x002AD76C File Offset: 0x002AB96C
		internal sealed override string TargetPath
		{
			get
			{
				return "embeddings";
			}
		}

		// Token: 0x1700368F RID: 13967
		// (get) Token: 0x0600DA36 RID: 55862 RVA: 0x002AD773 File Offset: 0x002AB973
		internal sealed override string TargetName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x17003690 RID: 13968
		// (get) Token: 0x0600DA37 RID: 55863 RVA: 0x002958E8 File Offset: 0x00293AE8
		internal sealed override string TargetFileExtension
		{
			get
			{
				return ".bin";
			}
		}

		// Token: 0x17003691 RID: 13969
		// (get) Token: 0x0600DA38 RID: 55864 RVA: 0x002A9132 File Offset: 0x002A7332
		public IEnumerable<EmbeddedControlPersistenceBinaryDataPart> EmbeddedControlPersistenceBinaryDataParts
		{
			get
			{
				return base.GetPartsOfType<EmbeddedControlPersistenceBinaryDataPart>();
			}
		}

		// Token: 0x17003692 RID: 13970
		// (get) Token: 0x0600DA39 RID: 55865 RVA: 0x00002105 File Offset: 0x00000305
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04006BB9 RID: 27577
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/control";

		// Token: 0x04006BBA RID: 27578
		internal const string TargetPathConstant = "embeddings";

		// Token: 0x04006BBB RID: 27579
		internal const string TargetNameConstant = "control";

		// Token: 0x04006BBC RID: 27580
		internal const string TargetFileExtensionConstant = ".bin";

		// Token: 0x04006BBD RID: 27581
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BBE RID: 27582
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;
	}
}
