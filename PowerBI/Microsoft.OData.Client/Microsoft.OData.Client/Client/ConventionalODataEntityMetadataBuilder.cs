using System;
using System.Text;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client
{
	// Token: 0x02000029 RID: 41
	internal sealed class ConventionalODataEntityMetadataBuilder : ODataResourceMetadataBuilder
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00007BE7 File Offset: 0x00005DE7
		internal ConventionalODataEntityMetadataBuilder(Uri baseUri, string entitySetName, IEdmStructuredValue entityInstance, DataServiceUrlKeyDelimiter keyDelimiter)
			: this(UriResolver.CreateFromBaseUri(baseUri, "baseUri"), entitySetName, entityInstance, keyDelimiter)
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007C00 File Offset: 0x00005E00
		internal ConventionalODataEntityMetadataBuilder(UriResolver resolver, string entitySetName, IEdmStructuredValue entityInstance, DataServiceUrlKeyDelimiter keyDelimiter)
		{
			Util.CheckArgumentNullAndEmpty(entitySetName, "entitySetName");
			Util.CheckArgumentNull<IEdmStructuredValue>(entityInstance, "entityInstance");
			Util.CheckArgumentNull<DataServiceUrlKeyDelimiter>(keyDelimiter, "keyDelimiter");
			this.entitySetName = entitySetName;
			this.entityInstance = entityInstance;
			this.uriBuilder = new ConventionalODataEntityMetadataBuilder.ConventionalODataUriBuilder(resolver, keyDelimiter);
			this.baseUri = resolver.BaseUriOrNull;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007C60 File Offset: 0x00005E60
		internal override Uri GetEditLink()
		{
			Uri uri = this.uriBuilder.BuildEntitySetUri(this.baseUri, this.entitySetName);
			return this.uriBuilder.BuildEntityInstanceUri(uri, this.entityInstance);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007C99 File Offset: 0x00005E99
		internal override Uri GetId()
		{
			return this.GetEditLink();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003487 File Offset: 0x00001687
		internal override string GetETag()
		{
			return null;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003487 File Offset: 0x00001687
		internal override Uri GetReadLink()
		{
			return null;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007CA1 File Offset: 0x00005EA1
		internal override bool TryGetIdForSerialization(out Uri id)
		{
			id = null;
			return false;
		}

		// Token: 0x04000071 RID: 113
		private readonly IEdmStructuredValue entityInstance;

		// Token: 0x04000072 RID: 114
		private readonly string entitySetName;

		// Token: 0x04000073 RID: 115
		private readonly Uri baseUri;

		// Token: 0x04000074 RID: 116
		private readonly ConventionalODataEntityMetadataBuilder.ConventionalODataUriBuilder uriBuilder;

		// Token: 0x02000160 RID: 352
		private class ConventionalODataUriBuilder : ODataUriBuilder
		{
			// Token: 0x06000D4C RID: 3404 RVA: 0x0002E86A File Offset: 0x0002CA6A
			internal ConventionalODataUriBuilder(UriResolver resolver, DataServiceUrlKeyDelimiter urlKeyDelimiter)
			{
				this.resolver = resolver;
				this.urlKeyDelimiter = urlKeyDelimiter;
			}

			// Token: 0x06000D4D RID: 3405 RVA: 0x0002E880 File Offset: 0x0002CA80
			internal override Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
			{
				return this.resolver.GetEntitySetUri(entitySetName);
			}

			// Token: 0x06000D4E RID: 3406 RVA: 0x0002E890 File Offset: 0x0002CA90
			internal override Uri BuildEntityInstanceUri(Uri baseUri, IEdmStructuredValue entityInstance)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (baseUri != null)
				{
					stringBuilder.Append(UriUtil.UriToString(baseUri));
				}
				this.urlKeyDelimiter.AppendKeyExpression(entityInstance, stringBuilder);
				return UriUtil.CreateUri(stringBuilder.ToString(), UriKind.RelativeOrAbsolute);
			}

			// Token: 0x04000703 RID: 1795
			private readonly UriResolver resolver;

			// Token: 0x04000704 RID: 1796
			private readonly DataServiceUrlKeyDelimiter urlKeyDelimiter;
		}
	}
}
