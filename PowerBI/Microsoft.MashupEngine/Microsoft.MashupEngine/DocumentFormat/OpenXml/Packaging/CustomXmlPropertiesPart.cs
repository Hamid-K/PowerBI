using System;
using System.Collections.Generic;
using System.Diagnostics;
using DocumentFormat.OpenXml.CustomXmlDataProperties;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x0200219D RID: 8605
	internal class CustomXmlPropertiesPart : OpenXmlPart, IFixedContentTypePart
	{
		// Token: 0x0600DA1F RID: 55839 RVA: 0x002AD57C File Offset: 0x002AB77C
		internal sealed override IDictionary<string, PartConstraintRule> GetPartConstraint()
		{
			if (CustomXmlPropertiesPart._partConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomXmlPropertiesPart._partConstraint = dictionary;
			}
			return CustomXmlPropertiesPart._partConstraint;
		}

		// Token: 0x0600DA20 RID: 55840 RVA: 0x002AD5A4 File Offset: 0x002AB7A4
		internal sealed override IDictionary<string, PartConstraintRule> GetDataPartReferenceConstraint()
		{
			if (CustomXmlPropertiesPart._dataPartReferenceConstraint == null)
			{
				Dictionary<string, PartConstraintRule> dictionary = new Dictionary<string, PartConstraintRule>();
				CustomXmlPropertiesPart._dataPartReferenceConstraint = dictionary;
			}
			return CustomXmlPropertiesPart._dataPartReferenceConstraint;
		}

		// Token: 0x0600DA21 RID: 55841 RVA: 0x002A1AC5 File Offset: 0x0029FCC5
		protected internal CustomXmlPropertiesPart()
		{
		}

		// Token: 0x17003685 RID: 13957
		// (get) Token: 0x0600DA22 RID: 55842 RVA: 0x002AD5C9 File Offset: 0x002AB7C9
		public sealed override string RelationshipType
		{
			get
			{
				return "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps";
			}
		}

		// Token: 0x17003686 RID: 13958
		// (get) Token: 0x0600DA23 RID: 55843 RVA: 0x002AD5D0 File Offset: 0x002AB7D0
		public sealed override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.customXmlProperties+xml";
			}
		}

		// Token: 0x17003687 RID: 13959
		// (get) Token: 0x0600DA24 RID: 55844 RVA: 0x002A40FD File Offset: 0x002A22FD
		internal sealed override string TargetPath
		{
			get
			{
				return ".";
			}
		}

		// Token: 0x17003688 RID: 13960
		// (get) Token: 0x0600DA25 RID: 55845 RVA: 0x002AD5D7 File Offset: 0x002AB7D7
		internal sealed override string TargetName
		{
			get
			{
				return "itemProps";
			}
		}

		// Token: 0x17003689 RID: 13961
		// (get) Token: 0x0600DA26 RID: 55846 RVA: 0x00002139 File Offset: 0x00000339
		internal sealed override bool IsContentTypeFixed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700368A RID: 13962
		// (get) Token: 0x0600DA27 RID: 55847 RVA: 0x002AD5DE File Offset: 0x002AB7DE
		// (set) Token: 0x0600DA28 RID: 55848 RVA: 0x002AD5E6 File Offset: 0x002AB7E6
		internal override OpenXmlPartRootElement _rootElement
		{
			get
			{
				return this._rootEle;
			}
			set
			{
				this._rootEle = value as DataStoreItem;
			}
		}

		// Token: 0x1700368B RID: 13963
		// (get) Token: 0x0600DA29 RID: 55849 RVA: 0x002AD5F4 File Offset: 0x002AB7F4
		internal override OpenXmlPartRootElement PartRootElement
		{
			get
			{
				return this.DataStoreItem;
			}
		}

		// Token: 0x1700368C RID: 13964
		// (get) Token: 0x0600DA2A RID: 55850 RVA: 0x002AD5FC File Offset: 0x002AB7FC
		// (set) Token: 0x0600DA2B RID: 55851 RVA: 0x002A3296 File Offset: 0x002A1496
		public DataStoreItem DataStoreItem
		{
			get
			{
				if (this._rootEle == null)
				{
					base.LoadDomTree<DataStoreItem>();
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

		// Token: 0x04006BB2 RID: 27570
		internal const string RelationshipTypeConstant = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps";

		// Token: 0x04006BB3 RID: 27571
		internal const string ContentTypeConstant = "application/vnd.openxmlformats-officedocument.customXmlProperties+xml";

		// Token: 0x04006BB4 RID: 27572
		internal const string TargetPathConstant = ".";

		// Token: 0x04006BB5 RID: 27573
		internal const string TargetNameConstant = "itemProps";

		// Token: 0x04006BB6 RID: 27574
		private static Dictionary<string, PartConstraintRule> _partConstraint;

		// Token: 0x04006BB7 RID: 27575
		private static Dictionary<string, PartConstraintRule> _dataPartReferenceConstraint;

		// Token: 0x04006BB8 RID: 27576
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DataStoreItem _rootEle;
	}
}
