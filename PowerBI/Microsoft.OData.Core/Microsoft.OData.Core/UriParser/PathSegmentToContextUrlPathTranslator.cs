using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.Evaluation;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001DF RID: 479
	internal sealed class PathSegmentToContextUrlPathTranslator : PathSegmentTranslator<string>
	{
		// Token: 0x0600158A RID: 5514 RVA: 0x0003DD71 File Offset: 0x0003BF71
		private PathSegmentToContextUrlPathTranslator(bool keyAsSegment)
		{
			this.KeySerializer = KeySerializer.Create(keyAsSegment);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0003DD88 File Offset: 0x0003BF88
		public override string Translate(TypeSegment segment)
		{
			IEdmType edmType = segment.EdmType;
			IEdmCollectionType edmCollectionType = edmType as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				edmType = edmCollectionType.ElementType.Definition;
			}
			return "/" + edmType.FullTypeName();
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0003DDC2 File Offset: 0x0003BFC2
		public override string Translate(NavigationPropertySegment segment)
		{
			return "/" + segment.NavigationProperty.Name;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0003DDD9 File Offset: 0x0003BFD9
		public override string Translate(EntitySetSegment segment)
		{
			return "/" + segment.EntitySet.Name;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0003DDF0 File Offset: 0x0003BFF0
		public override string Translate(SingletonSegment segment)
		{
			return "/" + segment.Singleton.Name;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0003DE08 File Offset: 0x0003C008
		public override string Translate(KeySegment segment)
		{
			List<KeyValuePair<string, object>> list = segment.Keys.ToList<KeyValuePair<string, object>>();
			StringBuilder stringBuilder = new StringBuilder();
			this.KeySerializer.AppendKeyExpression<KeyValuePair<string, object>>(stringBuilder, new Collection<KeyValuePair<string, object>>(list), (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => p.Value);
			return stringBuilder.ToString();
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0003DE7D File Offset: 0x0003C07D
		public override string Translate(PropertySegment segment)
		{
			return "/" + segment.Property.Name;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0003DE94 File Offset: 0x0003C094
		public override string Translate(AnnotationSegment segment)
		{
			return "/" + segment.Term.FullName();
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0003DEAB File Offset: 0x0003C0AB
		public override string Translate(OperationSegment segment)
		{
			return "/" + segment.Operations.OperationGroupFullName();
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(OperationImportSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(DynamicPathSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(CountSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(FilterSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(ReferenceSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(EachSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(NavigationPropertyLinkSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(ValueSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(BatchSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(BatchReferenceSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(MetadataSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(PathTemplateSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x04000A01 RID: 2561
		internal static readonly PathSegmentToContextUrlPathTranslator DefaultInstance = new PathSegmentToContextUrlPathTranslator(false);

		// Token: 0x04000A02 RID: 2562
		internal static readonly PathSegmentToContextUrlPathTranslator KeyAsSegmentInstance = new PathSegmentToContextUrlPathTranslator(true);

		// Token: 0x04000A03 RID: 2563
		private KeySerializer KeySerializer;
	}
}
