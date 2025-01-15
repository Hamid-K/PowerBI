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
	// Token: 0x020001E0 RID: 480
	internal sealed class PathSegmentToResourcePathTranslator : PathSegmentTranslator<string>
	{
		// Token: 0x060015A0 RID: 5536 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		public PathSegmentToResourcePathTranslator(ODataUrlKeyDelimiter odataUrlKeyDelimiter)
		{
			this.KeySerializer = KeySerializer.Create(odataUrlKeyDelimiter.EnableKeyAsSegment);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0003DF08 File Offset: 0x0003C108
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

		// Token: 0x060015A2 RID: 5538 RVA: 0x0003DDC2 File Offset: 0x0003BFC2
		public override string Translate(NavigationPropertySegment segment)
		{
			return "/" + segment.NavigationProperty.Name;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0003DDD9 File Offset: 0x0003BFD9
		public override string Translate(EntitySetSegment segment)
		{
			return "/" + segment.EntitySet.Name;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0003DDF0 File Offset: 0x0003BFF0
		public override string Translate(SingletonSegment segment)
		{
			return "/" + segment.Singleton.Name;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0003DF44 File Offset: 0x0003C144
		public override string Translate(KeySegment segment)
		{
			List<KeyValuePair<string, object>> list = segment.Keys.ToList<KeyValuePair<string, object>>();
			StringBuilder stringBuilder = new StringBuilder();
			this.KeySerializer.AppendKeyExpression<KeyValuePair<string, object>>(stringBuilder, new Collection<KeyValuePair<string, object>>(list), (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => p.Value);
			return stringBuilder.ToString();
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0003DE7D File Offset: 0x0003C07D
		public override string Translate(PropertySegment segment)
		{
			return "/" + segment.Property.Name;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0003DE94 File Offset: 0x0003C094
		public override string Translate(AnnotationSegment segment)
		{
			return "/" + segment.Term.FullName();
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0003DFBC File Offset: 0x0003C1BC
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

		// Token: 0x060015A9 RID: 5545 RVA: 0x0003E0AC File Offset: 0x0003C2AC
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

		// Token: 0x060015AA RID: 5546 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(DynamicPathSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(CountSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0003E190 File Offset: 0x0003C390
		public override string Translate(FilterSegment segment)
		{
			return "/" + segment.LiteralText;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(ReferenceSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(EachSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0003E1A2 File Offset: 0x0003C3A2
		public override string Translate(NavigationPropertyLinkSegment segment)
		{
			return "/" + segment.NavigationProperty.Name + "/" + "$ref";
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(ValueSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(BatchSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0003E1C3 File Offset: 0x0003C3C3
		public override string Translate(BatchReferenceSegment segment)
		{
			return "/" + segment.ContentId;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(MetadataSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		public override string Translate(PathTemplateSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x04000A04 RID: 2564
		private KeySerializer KeySerializer;
	}
}
