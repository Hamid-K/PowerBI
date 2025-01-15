using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D6A RID: 3434
	internal abstract class SetContext
	{
		// Token: 0x06005D3D RID: 23869 RVA: 0x00142DA3 File Offset: 0x00140FA3
		public SetContext(Set set)
			: this(set, EmptyArray<Microsoft.Mashup.Engine1.Library.Cube.ParameterArguments>.Instance)
		{
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x00142DB1 File Offset: 0x00140FB1
		public SetContext(Set set, IList<ParameterArguments> arguments)
		{
			this.set = set;
			this.arguments = arguments;
		}

		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x06005D3F RID: 23871 RVA: 0x00142DC7 File Offset: 0x00140FC7
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x06005D40 RID: 23872 RVA: 0x00142DCF File Offset: 0x00140FCF
		public IList<ParameterArguments> ParameterArguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x06005D41 RID: 23873 RVA: 0x001355BA File Offset: 0x001337BA
		public virtual TableValue DirectQueryCapabilities
		{
			get
			{
				return CapabilityModule.DirectQueryCapabilities.From.Invoke(TableValue.Empty).AsTable;
			}
		}

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x06005D42 RID: 23874
		public abstract TableValue DisplayFolders { get; }

		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x06005D43 RID: 23875
		public abstract TableValue MeasureGroups { get; }

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x06005D44 RID: 23876
		public abstract TableValue Dimensions { get; }

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x06005D45 RID: 23877
		public abstract TableValue Measures { get; }

		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x06005D46 RID: 23878
		public abstract SetContextProvider ContextProvider { get; }

		// Token: 0x06005D47 RID: 23879
		public abstract TableValue GetParameters(CubeValue cube);

		// Token: 0x06005D48 RID: 23880
		public abstract TableValue GetAvailableProperties();

		// Token: 0x06005D49 RID: 23881
		public abstract TableValue GetAvailableMeasureProperties();

		// Token: 0x06005D4A RID: 23882
		public abstract IEnumerator<IValueReference> Evaluate();

		// Token: 0x06005D4B RID: 23883 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void ReportFoldingFailure()
		{
		}

		// Token: 0x04003363 RID: 13155
		private readonly Set set;

		// Token: 0x04003364 RID: 13156
		private readonly IList<ParameterArguments> arguments;
	}
}
