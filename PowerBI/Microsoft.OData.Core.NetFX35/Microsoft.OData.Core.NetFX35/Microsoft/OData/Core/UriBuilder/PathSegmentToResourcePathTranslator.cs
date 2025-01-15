using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriBuilder
{
	// Token: 0x020001BF RID: 447
	internal sealed class PathSegmentToResourcePathTranslator : PathSegmentTranslator<string>
	{
		// Token: 0x060010BD RID: 4285 RVA: 0x0003A396 File Offset: 0x00038596
		public PathSegmentToResourcePathTranslator(UrlConvention urlConventions)
		{
			this.KeySerializer = KeySerializer.Create(urlConventions);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0003A3AC File Offset: 0x000385AC
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

		// Token: 0x060010BF RID: 4287 RVA: 0x0003A3E6 File Offset: 0x000385E6
		public override string Translate(NavigationPropertySegment segment)
		{
			return "/" + segment.NavigationProperty.Name;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0003A3FD File Offset: 0x000385FD
		public override string Translate(EntitySetSegment segment)
		{
			return "/" + segment.EntitySet.Name;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0003A414 File Offset: 0x00038614
		public override string Translate(SingletonSegment segment)
		{
			return "/" + segment.Singleton.Name;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0003A440 File Offset: 0x00038640
		public override string Translate(KeySegment segment)
		{
			List<KeyValuePair<string, object>> list = Enumerable.ToList<KeyValuePair<string, object>>(segment.Keys);
			StringBuilder stringBuilder = new StringBuilder();
			this.KeySerializer.AppendKeyExpression<KeyValuePair<string, object>>(stringBuilder, new Collection<KeyValuePair<string, object>>(list), (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => p.Value);
			return stringBuilder.ToString();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0003A4B1 File Offset: 0x000386B1
		public override string Translate(PropertySegment segment)
		{
			return "/" + segment.Property.Name;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003A4C8 File Offset: 0x000386C8
		public override string Translate(OperationSegment segment)
		{
			string text = null;
			NodeToStringBuilder nodeToStringBuilder = new NodeToStringBuilder();
			foreach (OperationSegmentParameter operationSegmentParameter in segment.Parameters)
			{
				string text2 = nodeToStringBuilder.TranslateNode((QueryNode)operationSegmentParameter.Value);
				text = string.Concat(new string[]
				{
					text,
					string.IsNullOrEmpty(text) ? null : ",",
					operationSegmentParameter.Name,
					"=",
					text2
				});
			}
			if (!string.IsNullOrEmpty(text))
			{
				return string.Concat(new string[]
				{
					"/",
					segment.Operations.OperationGroupFullName(),
					"(",
					text,
					")"
				});
			}
			return "/" + segment.Operations.OperationGroupFullName();
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003A5CC File Offset: 0x000387CC
		public override string Translate(OperationImportSegment segment)
		{
			NodeToStringBuilder nodeToStringBuilder = new NodeToStringBuilder();
			string text = null;
			foreach (OperationSegmentParameter operationSegmentParameter in segment.Parameters)
			{
				string text2 = nodeToStringBuilder.TranslateNode((QueryNode)operationSegmentParameter.Value);
				text = string.Concat(new string[]
				{
					text,
					string.IsNullOrEmpty(text) ? null : ",",
					operationSegmentParameter.Name,
					"=",
					text2
				});
			}
			if (!string.IsNullOrEmpty(text))
			{
				return string.Concat(new string[] { "/", segment.Identifier, "(", text, ")" });
			}
			return "/" + segment.Identifier;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003A6C4 File Offset: 0x000388C4
		public override string Translate(OpenPropertySegment segment)
		{
			return "/" + segment.PropertyName;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003A6D6 File Offset: 0x000388D6
		public override string Translate(CountSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003A6E8 File Offset: 0x000388E8
		public override string Translate(NavigationPropertyLinkSegment segment)
		{
			return "/" + segment.NavigationProperty.Name + "/" + "$ref";
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003A709 File Offset: 0x00038909
		public override string Translate(ValueSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003A71B File Offset: 0x0003891B
		public override string Translate(BatchSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003A72D File Offset: 0x0003892D
		public override string Translate(BatchReferenceSegment segment)
		{
			return "/" + segment.ContentId;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0003A73F File Offset: 0x0003893F
		public override string Translate(MetadataSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x0400076E RID: 1902
		private KeySerializer KeySerializer;
	}
}
