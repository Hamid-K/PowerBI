using System;
using System.Xml.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000204 RID: 516
	public sealed class EdmHierarchyLevel
	{
		// Token: 0x06001835 RID: 6197 RVA: 0x00042A94 File Offset: 0x00040C94
		internal EdmHierarchyLevel(EdmHierarchy hierarchy, XElement element, EntityType entity)
		{
			ArgumentValidation.CheckNotNull<EdmHierarchy>(hierarchy, "hierarchy");
			ArgumentValidation.CheckNotNull<XElement>(element, "element");
			this._hierarchy = hierarchy;
			this._caption = element.GetStringAttributeOrDefault(Extensions.CaptionAttr, null);
			XAttribute xattribute = ArgumentValidation.CheckNotNull<XAttribute>(element.Attribute(Extensions.NameAttr), "Name");
			this._name = ArgumentValidation.CheckNotNullOrEmpty(xattribute.Value, "Name");
			XAttribute xattribute2 = element.Attribute(Extensions.ReferenceNameAttr);
			if (xattribute2 != null)
			{
				this._referenceName = xattribute2.Value;
			}
			XElement xelement = ArgumentValidation.CheckNotNull<XElement>(ArgumentValidation.CheckNotNull<XElement>(element.GetElementOrNull(Extensions.SourceElem), "Source").GetElementOrNull(Extensions.PropertyRefElem), "PropertyRef");
			this._source = ArgumentValidation.CheckNotNull<EdmField>(entity.GetMemberFromMemberRef(xelement) as EdmField, "Source Field");
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00042B65 File Offset: 0x00040D65
		public string Caption
		{
			get
			{
				return this._caption ?? this.Name;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00042B77 File Offset: 0x00040D77
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x00042B7F File Offset: 0x00040D7F
		public string ReferenceName
		{
			get
			{
				return this._referenceName ?? this._name;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00042B91 File Offset: 0x00040D91
		public EdmField Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00042B99 File Offset: 0x00040D99
		public EdmHierarchy ParentHierarchy
		{
			get
			{
				return this._hierarchy;
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00042BA1 File Offset: 0x00040DA1
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000CFE RID: 3326
		private readonly string _caption;

		// Token: 0x04000CFF RID: 3327
		private readonly string _name;

		// Token: 0x04000D00 RID: 3328
		private readonly string _referenceName;

		// Token: 0x04000D01 RID: 3329
		private readonly EdmField _source;

		// Token: 0x04000D02 RID: 3330
		private readonly EdmHierarchy _hierarchy;
	}
}
