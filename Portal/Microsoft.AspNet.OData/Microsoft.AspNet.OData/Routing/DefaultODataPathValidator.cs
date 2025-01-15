using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000071 RID: 113
	public class DefaultODataPathValidator : PathSegmentHandler
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0000DC7D File Offset: 0x0000BE7D
		public DefaultODataPathValidator(IEdmModel model)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			this._edmModel = model;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(EntitySetSegment segment)
		{
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(KeySegment segment)
		{
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		public override void Handle(NavigationPropertyLinkSegment segment)
		{
			if (EdmLibHelpers.IsNotNavigable(segment.NavigationProperty, this._edmModel))
			{
				throw new ODataException(Error.Format(SRResources.NotNavigablePropertyUsedInNavigation, new object[] { segment.NavigationProperty.Name }));
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000DCD5 File Offset: 0x0000BED5
		public override void Handle(NavigationPropertySegment segment)
		{
			if (EdmLibHelpers.IsNotNavigable(segment.NavigationProperty, this._edmModel))
			{
				throw new ODataException(Error.Format(SRResources.NotNavigablePropertyUsedInNavigation, new object[] { segment.NavigationProperty.Name }));
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(DynamicPathSegment segment)
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(OperationImportSegment segment)
		{
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(OperationSegment segment)
		{
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000DD10 File Offset: 0x0000BF10
		public override void Handle(PathTemplateSegment segment)
		{
			string text2;
			string text = segment.TranslatePathTemplateSegment(out text2);
			if (text == null || !(text == "dynamicproperty"))
			{
				throw new ODataException(Error.Format(SRResources.InvalidAttributeRoutingTemplateSegment, new object[] { segment.LiteralText }));
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(PropertySegment segment)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(SingletonSegment segment)
		{
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(TypeSegment segment)
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(ValueSegment segment)
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(CountSegment segment)
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(BatchSegment segment)
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public override void Handle(MetadataSegment segment)
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public virtual void Handle(UnresolvedPathSegment segment)
		{
		}

		// Token: 0x040000E2 RID: 226
		private readonly IEdmModel _edmModel;
	}
}
