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
	// Token: 0x02000190 RID: 400
	internal sealed class PathSegmentToContextUrlPathTranslator : PathSegmentTranslator<string>
	{
		// Token: 0x06001031 RID: 4145 RVA: 0x0002D87E File Offset: 0x0002BA7E
		private PathSegmentToContextUrlPathTranslator(bool keyAsSegment)
		{
			this.KeySerializer = KeySerializer.Create(keyAsSegment);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0002D894 File Offset: 0x0002BA94
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

		// Token: 0x06001033 RID: 4147 RVA: 0x0002D8CE File Offset: 0x0002BACE
		public override string Translate(NavigationPropertySegment segment)
		{
			return "/" + segment.NavigationProperty.Name;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0002D8E5 File Offset: 0x0002BAE5
		public override string Translate(EntitySetSegment segment)
		{
			return "/" + segment.EntitySet.Name;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0002D8FC File Offset: 0x0002BAFC
		public override string Translate(SingletonSegment segment)
		{
			return "/" + segment.Singleton.Name;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0002D914 File Offset: 0x0002BB14
		public override string Translate(KeySegment segment)
		{
			List<KeyValuePair<string, object>> list = Enumerable.ToList<KeyValuePair<string, object>>(segment.Keys);
			StringBuilder stringBuilder = new StringBuilder();
			this.KeySerializer.AppendKeyExpression<KeyValuePair<string, object>>(stringBuilder, new Collection<KeyValuePair<string, object>>(list), (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => p.Value);
			return stringBuilder.ToString();
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0002D989 File Offset: 0x0002BB89
		public override string Translate(PropertySegment segment)
		{
			return "/" + segment.Property.Name;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		public override string Translate(OperationSegment segment)
		{
			return "/" + segment.Operations.OperationGroupFullName();
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00017B54 File Offset: 0x00015D54
		public override string Translate(OperationImportSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0002D9B7 File Offset: 0x0002BBB7
		public override string Translate(DynamicPathSegment segment)
		{
			return "/" + segment.Identifier;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00017B54 File Offset: 0x00015D54
		public override string Translate(CountSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00017B54 File Offset: 0x00015D54
		public override string Translate(NavigationPropertyLinkSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00017B54 File Offset: 0x00015D54
		public override string Translate(ValueSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00017B54 File Offset: 0x00015D54
		public override string Translate(BatchSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00017B54 File Offset: 0x00015D54
		public override string Translate(BatchReferenceSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00017B54 File Offset: 0x00015D54
		public override string Translate(MetadataSegment segment)
		{
			return string.Empty;
		}

		// Token: 0x040008A4 RID: 2212
		internal static readonly PathSegmentToContextUrlPathTranslator DefaultInstance = new PathSegmentToContextUrlPathTranslator(false);

		// Token: 0x040008A5 RID: 2213
		internal static readonly PathSegmentToContextUrlPathTranslator KeyAsSegmentInstance = new PathSegmentToContextUrlPathTranslator(true);

		// Token: 0x040008A6 RID: 2214
		private KeySerializer KeySerializer;
	}
}
