using System;
using System.Collections.Generic;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000061 RID: 97
	public sealed class ODataResourceSet : ODataResourceSetBase
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000A0A2 File Offset: 0x000082A2
		public IEnumerable<ODataAction> Actions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000A0AA File Offset: 0x000082AA
		public IEnumerable<ODataFunction> Functions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000A0B2 File Offset: 0x000082B2
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000A0ED File Offset: 0x000082ED
		public string TypeName
		{
			get
			{
				if (this.typeName == null && this.SerializationInfo != null && this.SerializationInfo.ExpectedTypeName != null)
				{
					this.typeName = EdmLibraryExtensions.GetCollectionTypeName(this.serializationInfo.ExpectedTypeName);
				}
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000A0F6 File Offset: 0x000082F6
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000A0FE File Offset: 0x000082FE
		internal ODataResourceSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataResourceSerializationInfo.Validate(value);
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A10C File Offset: 0x0000830C
		public void AddAction(ODataAction action)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataAction>(action, "action");
			if (!this.actions.Contains(action))
			{
				this.actions.Add(action);
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A134 File Offset: 0x00008334
		public void AddFunction(ODataFunction function)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFunction>(function, "function");
			if (!this.functions.Contains(function))
			{
				this.functions.Add(function);
			}
		}

		// Token: 0x040001AF RID: 431
		private List<ODataAction> actions = new List<ODataAction>();

		// Token: 0x040001B0 RID: 432
		private List<ODataFunction> functions = new List<ODataFunction>();

		// Token: 0x040001B1 RID: 433
		private ODataResourceSerializationInfo serializationInfo;

		// Token: 0x040001B2 RID: 434
		private string typeName;
	}
}
