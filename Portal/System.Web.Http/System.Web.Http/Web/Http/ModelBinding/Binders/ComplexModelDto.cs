using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200006D RID: 109
	public class ComplexModelDto
	{
		// Token: 0x060002EC RID: 748 RVA: 0x000086BC File Offset: 0x000068BC
		public ComplexModelDto(ModelMetadata modelMetadata, IEnumerable<ModelMetadata> propertyMetadata)
		{
			if (modelMetadata == null)
			{
				throw Error.ArgumentNull("modelMetadata");
			}
			if (propertyMetadata == null)
			{
				throw Error.ArgumentNull("propertyMetadata");
			}
			this.ModelMetadata = modelMetadata;
			this.PropertyMetadata = new Collection<ModelMetadata>(propertyMetadata.ToList<ModelMetadata>());
			this.Results = new Dictionary<ModelMetadata, ComplexModelDtoResult>();
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000870E File Offset: 0x0000690E
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00008716 File Offset: 0x00006916
		public ModelMetadata ModelMetadata { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000871F File Offset: 0x0000691F
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00008727 File Offset: 0x00006927
		public Collection<ModelMetadata> PropertyMetadata { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00008730 File Offset: 0x00006930
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00008738 File Offset: 0x00006938
		public IDictionary<ModelMetadata, ComplexModelDtoResult> Results { get; private set; }
	}
}
