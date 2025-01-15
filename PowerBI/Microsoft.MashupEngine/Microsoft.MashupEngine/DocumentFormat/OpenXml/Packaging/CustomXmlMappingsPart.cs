using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200216F RID: 8559
	internal class CustomXmlMappingsPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600D66E RID: 54894 RVA: 0x002A79D0 File Offset: 0x002A5BD0
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CustomXmlMappingsPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomXmlMappingsPart._partConstraint = dictionary;
			}
			return CustomXmlMappingsPart._partConstraint;
		}

		// Token: 0x0600D66F RID: 54895 RVA: 0x002A79F8 File Offset: 0x002A5BF8
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CustomXmlMappingsPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomXmlMappingsPart._dataPartReferenceConstraint = dictionary;
			}
			return CustomXmlMappingsPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600D670 RID: 54896 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CustomXmlMappingsPart()
		{
		}

		// Token: 0x17003480 RID: 13440
		// (get) Token: 0x0600D671 RID: 54897 RVA: 0x002A7A1D File Offset: 0x002A5C1D
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/xmlMaps";
			}
		}

		// Token: 0x17003481 RID: 13441
		// (get) Token: 0x0600D672 RID: 54898 RVA: 0x002A7A24 File Offset: 0x002A5C24
		public sealed override string ContentType
		{
			get
			{
				return "application/xml";
			}
		}

		// Token: 0x17003482 RID: 13442
		// (get) Token: 0x0600D673 RID: 54899 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003483 RID: 13443
		// (get) Token: 0x0600D674 RID: 54900 RVA: 0x002A7A2B File Offset: 0x002A5C2B
		internal sealed override string TargetName
		{
			get
			{
				return "xmlMaps";
			}
		}

		// Token: 0x17003484 RID: 13444
		// (get) Token: 0x0600D675 RID: 54901 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003485 RID: 13445
		// (get) Token: 0x0600D676 RID: 54902 RVA: 0x002A7A32 File Offset: 0x002A5C32
		// (set) Token: 0x0600D677 RID: 54903 RVA: 0x002A7A3A File Offset: 0x002A5C3A
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as MapInfo;
			}
		}

		// Token: 0x17003486 RID: 13446
		// (get) Token: 0x0600D678 RID: 54904 RVA: 0x002A7A48 File Offset: 0x002A5C48
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.MapInfo;
			}
		}

		// Token: 0x17003487 RID: 13447
		// (get) Token: 0x0600D679 RID: 54905 RVA: 0x002A7A50 File Offset: 0x002A5C50
		// (set) Token: 0x0600D67A RID: 54906 RVA: 0x002A3296 File Offset: 0x002A1496
		public MapInfo MapInfo
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<MapInfo>();
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

		// Token: 0x04006A73 RID: 27251
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/xmlMaps";

		// Token: 0x04006A74 RID: 27252
		internal const string ContentTypeConstant = "application/xml";

		// Token: 0x04006A75 RID: 27253
		internal const string TargetPathConstant = ".";

		// Token: 0x04006A76 RID: 27254
		internal const string TargetNameConstant = "xmlMaps";

		// Token: 0x04006A77 RID: 27255
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006A78 RID: 27256
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006A79 RID: 27257
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private MapInfo _rootEle;
	}
}
