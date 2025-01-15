using System;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000148 RID: 328
	public sealed class TmdlSerializationOptions : MetadataSerializationOptions
	{
		// Token: 0x06001547 RID: 5447 RVA: 0x0008F561 File Offset: 0x0008D761
		internal TmdlSerializationOptions()
		{
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0008F569 File Offset: 0x0008D769
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0008F571 File Offset: 0x0008D771
		public TmdlExpressionTrimStyle ExpressionTrimStyle { get; internal set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0008F57A File Offset: 0x0008D77A
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x0008F582 File Offset: 0x0008D782
		public bool ExcludeMetadataOrderHints { get; internal set; }

		// Token: 0x0600154C RID: 5452 RVA: 0x0008F58C File Offset: 0x0008D78C
		internal static bool ShouldOrderHintsBeIncludedInTmdlContent(MetadataSerializationOptions options)
		{
			if (options != null)
			{
				TmdlSerializationOptions tmdlSerializationOptions = options as TmdlSerializationOptions;
				if (tmdlSerializationOptions != null)
				{
					return !tmdlSerializationOptions.ExcludeMetadataOrderHints;
				}
			}
			return true;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0008F5B1 File Offset: 0x0008D7B1
		private protected override MetadataSerializationOptions CreateOptionsOfSameType()
		{
			return new TmdlSerializationOptions();
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0008F5B8 File Offset: 0x0008D7B8
		private protected override void CloneImpl(MetadataSerializationOptions options)
		{
			base.CloneImpl(options);
			TmdlSerializationOptions tmdlSerializationOptions = options as TmdlSerializationOptions;
			if (tmdlSerializationOptions == null)
			{
				throw TomInternalException.Create("This must be options of type {0}, but the argument is of type {1}!", new object[]
				{
					typeof(TmdlSerializationOptions).Name,
					options.GetType().Name
				});
			}
			tmdlSerializationOptions.ExpressionTrimStyle = this.ExpressionTrimStyle;
			tmdlSerializationOptions.ExcludeMetadataOrderHints = this.ExcludeMetadataOrderHints;
		}
	}
}
