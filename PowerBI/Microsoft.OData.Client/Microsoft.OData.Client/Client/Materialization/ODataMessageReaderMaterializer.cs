using System;
using System.Collections.Generic;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x0200010B RID: 267
	internal abstract class ODataMessageReaderMaterializer : ODataMaterializer
	{
		// Token: 0x06000B5C RID: 2908 RVA: 0x0002B1AF File Offset: 0x000293AF
		public ODataMessageReaderMaterializer(ODataMessageReader reader, IODataMaterializerContext context, Type expectedType, bool? singleResult)
			: base(context, expectedType)
		{
			this.messageReader = reader;
			this.SingleResult = singleResult;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00003487 File Offset: 0x00001687
		internal sealed override ODataResourceSet CurrentFeed
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00003487 File Offset: 0x00001687
		internal sealed override ODataResource CurrentEntry
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0002B1C8 File Offset: 0x000293C8
		internal sealed override bool IsEndOfStream
		{
			get
			{
				return this.hasReadValue;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0002988C File Offset: 0x00027A8C
		internal override long CountValue
		{
			get
			{
				throw new InvalidOperationException(Strings.MaterializeFromAtom_CountNotPresent);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0000A08D File Offset: 0x0000828D
		internal sealed override ProjectionPlan MaterializeEntryPlan
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002B1D0 File Offset: 0x000293D0
		protected sealed override bool IsDisposed
		{
			get
			{
				return this.messageReader == null;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0002B1DB File Offset: 0x000293DB
		protected override ODataFormat Format
		{
			get
			{
				return ODataUtils.GetReadFormat(this.messageReader);
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0000B028 File Offset: 0x00009228
		internal sealed override void ClearLog()
		{
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0000B028 File Offset: 0x00009228
		internal sealed override void ApplyLogToContext()
		{
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002B1E8 File Offset: 0x000293E8
		protected sealed override bool ReadImplementation()
		{
			if (!this.hasReadValue)
			{
				try
				{
					ClientEdmModel model = base.MaterializerContext.Model;
					Type type = base.ExpectedType;
					IEdmTypeReference edmTypeReference = model.GetOrCreateEdmType(type).ToEdmTypeReference(ClientTypeUtil.CanAssignNull(type));
					if (this.SingleResult != null && !this.SingleResult.Value && edmTypeReference.Definition.TypeKind != EdmTypeKind.Collection)
					{
						type = typeof(ICollection<>).MakeGenericType(new Type[] { type });
						edmTypeReference = model.GetOrCreateEdmType(type).ToEdmTypeReference(false);
					}
					IEdmTypeReference edmTypeReference2 = base.MaterializerContext.ResolveExpectedTypeForReading(type).ToEdmTypeReference(edmTypeReference.IsNullable);
					this.ReadWithExpectedType(edmTypeReference, edmTypeReference2);
				}
				catch (ODataErrorException ex)
				{
					throw new DataServiceClientException(Strings.Deserialize_ServerException(ex.Error.Message), ex);
				}
				catch (ODataException ex2)
				{
					throw new InvalidOperationException(ex2.Message, ex2);
				}
				catch (ArgumentException ex3)
				{
					throw new InvalidOperationException(ex3.Message, ex3);
				}
				finally
				{
					this.hasReadValue = true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002B31C File Offset: 0x0002951C
		protected sealed override void OnDispose()
		{
			if (this.messageReader != null)
			{
				this.messageReader.Dispose();
				this.messageReader = null;
			}
		}

		// Token: 0x06000B68 RID: 2920
		protected abstract void ReadWithExpectedType(IEdmTypeReference expectedClientType, IEdmTypeReference expectedReaderType);

		// Token: 0x0400063C RID: 1596
		protected readonly bool? SingleResult;

		// Token: 0x0400063D RID: 1597
		protected ODataMessageReader messageReader;

		// Token: 0x0400063E RID: 1598
		private bool hasReadValue;
	}
}
