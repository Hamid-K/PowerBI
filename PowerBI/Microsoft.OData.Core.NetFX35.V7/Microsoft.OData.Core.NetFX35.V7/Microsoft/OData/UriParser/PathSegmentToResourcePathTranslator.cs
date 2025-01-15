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
	// Token: 0x020001BD RID: 445
	internal sealed class PathSegmentToResourcePathTranslator : PathSegmentTranslator<string>
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x00030D84 File Offset: 0x0002EF84
		public PathSegmentToResourcePathTranslator(ODataUrlKeyDelimiter odataUrlKeyDelimiter)
		{
			this.KeySerializer = KeySerializer.Create(odataUrlKeyDelimiter.EnableKeyAsSegment);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00030DA0 File Offset: 0x0002EFA0
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

		// Token: 0x06001197 RID: 4503 RVA: 0x0002D8CE File Offset: 0x0002BACE
		public override string Translate(NavigationPropertySegment segment)
		{
			return "/" + segment.NavigationProperty.Name;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0002D8E5 File Offset: 0x0002BAE5
		public override string Translate(EntitySetSegment segment)
		{
			return "/" + segment.EntitySet.Name;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0002D8FC File Offset: 0x0002BAFC
		public override string Translate(SingletonSegment segment)
		{
			return "/" + segment.Singleton.Name;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00030DDC File Offset: 0x0002EFDC
		public override string Translate(KeySegment segment)
		{
			List<KeyValuePair<string, object>> list = Enumerable.ToList<KeyValuePair<string, object>>(segment.Keys);
			StringBuilder stringBuilder = new StringBuilder();
			this.KeySerializer.AppendKeyExpression<KeyValuePair<string, object>>(stringBuilder, new Collection<KeyValuePair<string, object>>(list), (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => p.Value);
			return stringBuilder.ToString();
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0002D989 File Offset: 0x0002BB89
		public override string Translate(PropertySegment segment)
		{
			return "/" + segment.Property.Name;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00030E54 File Offset: 0x0002F054
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

		// Token: 0x0600119D RID: 4509 RVA: 0x00030F44 File Offset: 0x0002F144
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

		// Token: 0x0600119E RID: 4510 RVA: 0x0002D9B7 File Offset: 0x0002BBB7
		public override string Translate(DynamicPathSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0002D9B7 File Offset: 0x0002BBB7
		public override string Translate(CountSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00031028 File Offset: 0x0002F228
		public override string Translate(NavigationPropertyLinkSegment segment)
		{
			return "/" + segment.NavigationProperty.Name + "/" + "$ref";
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0002D9B7 File Offset: 0x0002BBB7
		public override string Translate(ValueSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0002D9B7 File Offset: 0x0002BBB7
		public override string Translate(BatchSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00031049 File Offset: 0x0002F249
		public override string Translate(BatchReferenceSegment segment)
		{
			return "/" + segment.ContentId;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0002D9B7 File Offset: 0x0002BBB7
		public override string Translate(MetadataSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x040008F3 RID: 2291
		private KeySerializer KeySerializer;
	}
}
