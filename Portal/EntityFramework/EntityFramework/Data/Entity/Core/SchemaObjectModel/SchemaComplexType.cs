using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000311 RID: 785
	internal sealed class SchemaComplexType : StructuredType
	{
		// Token: 0x06002569 RID: 9577 RVA: 0x0006AD4D File Offset: 0x00068F4D
		internal SchemaComplexType(Schema parentElement)
			: base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x0006AD79 File Offset: 0x00068F79
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.BaseType != null && !(base.BaseType is SchemaComplexType))
			{
				base.AddError(ErrorCode.InvalidBaseType, EdmSchemaErrorSeverity.Error, Strings.InvalidBaseTypeForNestedType(base.BaseType.FQName, this.FQName));
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x0006ADB5 File Offset: 0x00068FB5
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "ValueAnnotation"))
			{
				this.SkipElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeAnnotation"))
			{
				this.SkipElement(reader);
				return true;
			}
			return false;
		}
	}
}
