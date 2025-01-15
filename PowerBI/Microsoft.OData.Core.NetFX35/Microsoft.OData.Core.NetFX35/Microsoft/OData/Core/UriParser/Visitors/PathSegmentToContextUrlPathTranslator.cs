using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000294 RID: 660
	internal sealed class PathSegmentToContextUrlPathTranslator : PathSegmentTranslator<string>
	{
		// Token: 0x060016A4 RID: 5796 RVA: 0x0004E5DC File Offset: 0x0004C7DC
		private PathSegmentToContextUrlPathTranslator(bool keyAsSegment)
		{
			this.KeySerializer = KeySerializer.Create(UrlConvention.CreateWithExplicitValue(keyAsSegment));
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0004E5F8 File Offset: 0x0004C7F8
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

		// Token: 0x060016A6 RID: 5798 RVA: 0x0004E632 File Offset: 0x0004C832
		public override string Translate(NavigationPropertySegment segment)
		{
			return "/" + segment.NavigationProperty.Name;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0004E649 File Offset: 0x0004C849
		public override string Translate(EntitySetSegment segment)
		{
			return "/" + segment.EntitySet.Name;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0004E660 File Offset: 0x0004C860
		public override string Translate(SingletonSegment segment)
		{
			return "/" + segment.Singleton.Name;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0004E68C File Offset: 0x0004C88C
		public override string Translate(KeySegment segment)
		{
			List<KeyValuePair<string, object>> list = Enumerable.ToList<KeyValuePair<string, object>>(segment.Keys);
			StringBuilder stringBuilder = new StringBuilder();
			this.KeySerializer.AppendKeyExpression<KeyValuePair<string, object>>(stringBuilder, new Collection<KeyValuePair<string, object>>(list), (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => p.Value);
			return stringBuilder.ToString();
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0004E6FD File Offset: 0x0004C8FD
		public override string Translate(PropertySegment segment)
		{
			return "/" + segment.Property.Name;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0004E714 File Offset: 0x0004C914
		public override string Translate(OperationSegment segment)
		{
			return "/" + segment.Operations.OperationGroupFullName();
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0004E72B File Offset: 0x0004C92B
		public override string Translate(OperationImportSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0004E732 File Offset: 0x0004C932
		public override string Translate(OpenPropertySegment segment)
		{
			return "/" + segment.PropertyName;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0004E744 File Offset: 0x0004C944
		public override string Translate(CountSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0004E74B File Offset: 0x0004C94B
		public override string Translate(NavigationPropertyLinkSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0004E752 File Offset: 0x0004C952
		public override string Translate(ValueSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0004E759 File Offset: 0x0004C959
		public override string Translate(BatchSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0004E760 File Offset: 0x0004C960
		public override string Translate(BatchReferenceSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0004E767 File Offset: 0x0004C967
		public override string Translate(MetadataSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x040009FA RID: 2554
		internal static readonly PathSegmentToContextUrlPathTranslator DefaultInstance = new PathSegmentToContextUrlPathTranslator(false);

		// Token: 0x040009FB RID: 2555
		internal static readonly PathSegmentToContextUrlPathTranslator KeyAsSegmentInstance = new PathSegmentToContextUrlPathTranslator(true);

		// Token: 0x040009FC RID: 2556
		private KeySerializer KeySerializer;
	}
}
