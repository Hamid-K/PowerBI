using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001D7 RID: 471
	internal static class MetadataWriterExtensions
	{
		// Token: 0x06001C0C RID: 7180 RVA: 0x000C3C37 File Offset: 0x000C1E37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateComplexPropertyScope(this IMetadataWriter writer, string propertyName, MetadataPropertyNature propertyNature)
		{
			return new MetadataWriterExtensions.ComplexPropertyScope(writer, propertyName, propertyNature);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000C3C41 File Offset: 0x000C1E41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateComplexPropertyCollectionScope(this IMetadataWriter writer, string collectionName, MetadataPropertyNature collectionNature)
		{
			return new MetadataWriterExtensions.ComplexPropertyCollectionScope(writer, collectionName, collectionNature);
		}

		// Token: 0x0200042B RID: 1067
		private sealed class ComplexPropertyScope : Disposable
		{
			// Token: 0x06002889 RID: 10377 RVA: 0x000EF81C File Offset: 0x000EDA1C
			public ComplexPropertyScope(IMetadataWriter writer, string propertyName, MetadataPropertyNature propertyNature)
			{
				writer.StartComplexProperty(propertyName, propertyNature);
				this.writer = writer;
			}

			// Token: 0x0600288A RID: 10378 RVA: 0x000EF834 File Offset: 0x000EDA34
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (this.writer != null)
					{
						this.writer.CompleteComplexProperty();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x04001399 RID: 5017
			private IMetadataWriter writer;
		}

		// Token: 0x0200042C RID: 1068
		private sealed class ComplexPropertyCollectionScope : Disposable
		{
			// Token: 0x0600288B RID: 10379 RVA: 0x000EF870 File Offset: 0x000EDA70
			public ComplexPropertyCollectionScope(IMetadataWriter writer, string collectionName, MetadataPropertyNature collectionNature)
			{
				writer.StartComplexPropertyCollection(collectionName, collectionNature);
				this.writer = writer;
			}

			// Token: 0x0600288C RID: 10380 RVA: 0x000EF888 File Offset: 0x000EDA88
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (this.writer != null)
					{
						this.writer.CompleteComplexPropertyCollection();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x0400139A RID: 5018
			private IMetadataWriter writer;
		}
	}
}
