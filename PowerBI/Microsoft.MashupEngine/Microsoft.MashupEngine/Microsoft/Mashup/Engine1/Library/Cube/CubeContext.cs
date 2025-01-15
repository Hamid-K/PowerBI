using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CCA RID: 3274
	internal abstract class CubeContext
	{
		// Token: 0x06005886 RID: 22662 RVA: 0x00135586 File Offset: 0x00133786
		public CubeContext(QueryCubeExpression expression)
			: this(expression, EmptyArray<Microsoft.Mashup.Engine1.Library.Cube.ParameterArguments>.Instance)
		{
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x00135594 File Offset: 0x00133794
		public CubeContext(QueryCubeExpression expression, IList<ParameterArguments> arguments)
		{
			this.expression = expression;
			this.arguments = arguments;
		}

		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x06005888 RID: 22664 RVA: 0x001355AA File Offset: 0x001337AA
		public QueryCubeExpression CubeExpression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06005889 RID: 22665 RVA: 0x001355B2 File Offset: 0x001337B2
		public IList<ParameterArguments> ParameterArguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x0600588A RID: 22666
		public abstract IEngineHost EngineHost { get; }

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x0600588B RID: 22667 RVA: 0x001355BA File Offset: 0x001337BA
		public virtual TableValue DirectQueryCapabilities
		{
			get
			{
				return CapabilityModule.DirectQueryCapabilities.From.Invoke(TableValue.Empty).AsTable;
			}
		}

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x0600588C RID: 22668
		public abstract TableValue DisplayFolders { get; }

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x0600588D RID: 22669
		public abstract TableValue MeasureGroups { get; }

		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x0600588E RID: 22670
		public abstract TableValue Dimensions { get; }

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x0600588F RID: 22671
		public abstract TableValue Measures { get; }

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x06005890 RID: 22672
		public abstract CubeContextProvider ContextProvider { get; }

		// Token: 0x06005891 RID: 22673
		public abstract TableValue GetAvailableProperties();

		// Token: 0x06005892 RID: 22674
		public abstract TableValue GetAvailableMeasureProperties();

		// Token: 0x06005893 RID: 22675
		public abstract IEnumerator<IValueReference> Evaluate();

		// Token: 0x06005894 RID: 22676 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetNativeQuery(out string nativeQuery)
		{
			nativeQuery = null;
			return false;
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetReader(out IPageReader reader)
		{
			reader = null;
			return false;
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x001355D0 File Offset: 0x001337D0
		public virtual TableValue GetParameters(CubeValue cube)
		{
			return CubeParametersTableValue.Empty;
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetQuery(out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void ReportFoldingFailure()
		{
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x001355D8 File Offset: 0x001337D8
		protected static Keys GetKeys(QueryCubeExpression expression)
		{
			if (expression.DimensionAttributes.Count == 0 && expression.Properties.Count == 0 && expression.Measures.Count == 0 && expression.MeasureProperties.Count == 0)
			{
				return Keys.Empty;
			}
			KeysBuilder keysBuilder = new KeysBuilder(expression.DimensionAttributes.Count + expression.Properties.Count + expression.Measures.Count + expression.MeasureProperties.Count);
			for (int i = 0; i < expression.DimensionAttributes.Count; i++)
			{
				keysBuilder.Add(expression.DimensionAttributes[i].Identifier);
			}
			for (int j = 0; j < expression.Properties.Count; j++)
			{
				keysBuilder.Add(expression.Properties[j].Identifier);
			}
			for (int k = 0; k < expression.Measures.Count; k++)
			{
				keysBuilder.Add(expression.Measures[k].Identifier);
			}
			for (int l = 0; l < expression.MeasureProperties.Count; l++)
			{
				keysBuilder.Add(expression.MeasureProperties[l].Identifier);
			}
			return keysBuilder.ToKeys();
		}

		// Token: 0x040031DF RID: 12767
		private readonly QueryCubeExpression expression;

		// Token: 0x040031E0 RID: 12768
		private readonly IList<ParameterArguments> arguments;
	}
}
