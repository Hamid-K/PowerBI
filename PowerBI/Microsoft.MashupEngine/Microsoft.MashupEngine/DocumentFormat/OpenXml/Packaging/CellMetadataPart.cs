using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x02002173 RID: 8563
	internal class CellMetadataPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D6B9 RID: 54969 RVA: 0x002A8094 File Offset: 0x002A6294
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CellMetadataPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CellMetadataPart._partConstraint = dictionary;
			}
			return CellMetadataPart._partConstraint;
		}

		// Token: 0x0600D6BA RID: 54970 RVA: 0x002A80BC File Offset: 0x002A62BC
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CellMetadataPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CellMetadataPart._dataPartReferenceConstraint = dictionary;
			}
			return CellMetadataPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D6BB RID: 54971 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CellMetadataPart()
		{
		}

		// Token: 0x170034AC RID: 13484
		// (get) Token: 0x0600D6BC RID: 54972 RVA: 0x002A80E1 File Offset: 0x002A62E1
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sheetMetadata";
			}
		}

		// Token: 0x170034AD RID: 13485
		// (get) Token: 0x0600D6BD RID: 54973 RVA: 0x002A80E8 File Offset: 0x002A62E8
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheetMetadata+xml";
			}
		}

		// Token: 0x170034AE RID: 13486
		// (get) Token: 0x0600D6BE RID: 54974 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x170034AF RID: 13487
		// (get) Token: 0x0600D6BF RID: 54975 RVA: 0x002A80EF File Offset: 0x002A62EF
		internal sealed override string TargetName
		{
			get
			{
				return "metadata";
			}
		}

		// Token: 0x170034B0 RID: 13488
		// (get) Token: 0x0600D6C0 RID: 54976 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170034B1 RID: 13489
		// (get) Token: 0x0600D6C1 RID: 54977 RVA: 0x002A80F6 File Offset: 0x002A62F6
		// (set) Token: 0x0600D6C2 RID: 54978 RVA: 0x002A80FE File Offset: 0x002A62FE
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as Metadata;
			}
		}

		// Token: 0x170034B2 RID: 13490
		// (get) Token: 0x0600D6C3 RID: 54979 RVA: 0x002A810C File Offset: 0x002A630C
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.Metadata;
			}
		}

		// Token: 0x170034B3 RID: 13491
		// (get) Token: 0x0600D6C4 RID: 54980 RVA: 0x002A8114 File Offset: 0x002A6314
		// (set) Token: 0x0600D6C5 RID: 54981 RVA: 0x002A3296 File Offset: 0x002A1496
		public Metadata Metadata
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<Metadata>();
				}
				return this._rootEle;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetDomTree(value);
			}
		}

		// Token: 0x04006A8F RID: 27279
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/sheetMetadata";

		// Token: 0x04006A90 RID: 27280
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheetMetadata+xml";

		// Token: 0x04006A91 RID: 27281
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A92 RID: 27282
		internal const string TargetNameConstant = "metadata";

		// Token: 0x04006A93 RID: 27283
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A94 RID: 27284
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A95 RID: 27285
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Metadata _rootEle;
	}
}
