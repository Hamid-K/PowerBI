using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200025F RID: 607
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class SuppressDbSetInitializationAttribute : Attribute
	{
	}
}
