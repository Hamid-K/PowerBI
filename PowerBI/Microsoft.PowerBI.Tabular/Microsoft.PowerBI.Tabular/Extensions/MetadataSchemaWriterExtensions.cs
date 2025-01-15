using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D6 RID: 470
	internal static class MetadataSchemaWriterExtensions
	{
		// Token: 0x06001C08 RID: 7176 RVA: 0x000C3C09 File Offset: 0x000C1E09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateMetadataObjectScope(this IMetadataSchemaWriter writer, ObjectType objectType, string choiceOption, string description, bool? additionalProperties)
		{
			return new MetadataSchemaWriterExtensions.MetadataObjectScope(writer, objectType, choiceOption, description, additionalProperties);
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000C3C16 File Offset: 0x000C1E16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateComplexPropertyScope(this IMetadataSchemaWriter writer, string propertyName, MetadataPropertyNature propertyNature, string choiceOption, string description, bool? additionalProperties)
		{
			return new MetadataSchemaWriterExtensions.ComplexPropertyScope(writer, propertyName, propertyNature, choiceOption, description, additionalProperties);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000C3C25 File Offset: 0x000C1E25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateCollectionScope(this IMetadataSchemaWriter writer, string collectionName, MetadataPropertyNature collectionNature)
		{
			return new MetadataSchemaWriterExtensions.CollectionScope(writer, collectionName, collectionNature);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000C3C2F File Offset: 0x000C1E2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateChoiceScope(this IMetadataSchemaWriter writer)
		{
			return new MetadataSchemaWriterExtensions.ChoiceScope(writer);
		}

		// Token: 0x02000427 RID: 1063
		private sealed class MetadataObjectScope : Disposable
		{
			// Token: 0x06002881 RID: 10369 RVA: 0x000EF6AF File Offset: 0x000ED8AF
			public MetadataObjectScope(IMetadataSchemaWriter writer, ObjectType objectType, string choiceOption, string description, bool? additionalProperties)
			{
				writer.StartMetadataObjectScope(objectType, choiceOption, description);
				this.writer = writer;
				this.additionalProperties = additionalProperties;
			}

			// Token: 0x06002882 RID: 10370 RVA: 0x000EF6D0 File Offset: 0x000ED8D0
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (this.writer != null)
					{
						this.writer.CompleteMetadataObjectScope(this.additionalProperties);
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x04001393 RID: 5011
			private IMetadataSchemaWriter writer;

			// Token: 0x04001394 RID: 5012
			private bool? additionalProperties;
		}

		// Token: 0x02000428 RID: 1064
		private sealed class ComplexPropertyScope : Disposable
		{
			// Token: 0x06002883 RID: 10371 RVA: 0x000EF710 File Offset: 0x000ED910
			public ComplexPropertyScope(IMetadataSchemaWriter writer, string propertyName, MetadataPropertyNature propertyNature, string choiceOption, string description, bool? additionalProperties)
			{
				writer.StartComplexPropertyScope(propertyName, propertyNature, choiceOption, description);
				this.writer = writer;
				this.additionalProperties = additionalProperties;
			}

			// Token: 0x06002884 RID: 10372 RVA: 0x000EF734 File Offset: 0x000ED934
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (this.writer != null)
					{
						this.writer.CompleteComplexPropertyScope(this.additionalProperties);
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x04001395 RID: 5013
			private IMetadataSchemaWriter writer;

			// Token: 0x04001396 RID: 5014
			private bool? additionalProperties;
		}

		// Token: 0x02000429 RID: 1065
		private sealed class CollectionScope : Disposable
		{
			// Token: 0x06002885 RID: 10373 RVA: 0x000EF774 File Offset: 0x000ED974
			public CollectionScope(IMetadataSchemaWriter writer, string collectionName, MetadataPropertyNature collectionNature)
			{
				writer.StartCollectionScope(collectionName, collectionNature);
				this.writer = writer;
			}

			// Token: 0x06002886 RID: 10374 RVA: 0x000EF78C File Offset: 0x000ED98C
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (this.writer != null)
					{
						this.writer.CompleteCollectionScope();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x04001397 RID: 5015
			private IMetadataSchemaWriter writer;
		}

		// Token: 0x0200042A RID: 1066
		private sealed class ChoiceScope : Disposable
		{
			// Token: 0x06002887 RID: 10375 RVA: 0x000EF7C8 File Offset: 0x000ED9C8
			public ChoiceScope(IMetadataSchemaWriter writer)
			{
				writer.StartChoiceScope();
				this.writer = writer;
			}

			// Token: 0x06002888 RID: 10376 RVA: 0x000EF7E0 File Offset: 0x000ED9E0
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (this.writer != null)
					{
						this.writer.CompleteChoiceScope();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x04001398 RID: 5016
			private IMetadataSchemaWriter writer;
		}
	}
}
