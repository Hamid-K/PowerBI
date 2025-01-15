using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000D3 RID: 211
	[DataContract(Name = "TemplateItem", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	[KnownType(typeof(StaticItem))]
	[KnownType(typeof(ConstantSlotItem))]
	[KnownType(typeof(SelectionSlotItem))]
	public abstract class PhrasingTemplateItem
	{
	}
}
