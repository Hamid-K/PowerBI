using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000D6 RID: 214
	internal sealed class FunctionDescriptor : IEquatable<FunctionDescriptor>
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		internal FunctionDescriptor(string name, string backingFunctionName, FunctionCategory functionCategory, IEnumerable<ArgumentDescriptor> arguments, bool canBeHandledByProcessing, bool canBeHandledByQuery, bool ignoresNulls, bool returnsNulls, bool dataTableCompatible, bool batchQueryCompatible)
		{
			Contract.RetailAssert(this.IsValidOptionalArguments(arguments), "optional arguments are invalid");
			this.Name = name;
			this.BackingFunctionName = backingFunctionName;
			this.FunctionCategory = functionCategory;
			this.Arguments = arguments.ToReadOnlyCollection<ArgumentDescriptor>();
			this.CanBeHandledByProcessing = canBeHandledByProcessing;
			this.CanBeHandledByQuery = canBeHandledByQuery;
			this.IgnoresNulls = ignoresNulls;
			this.ReturnsNulls = returnsNulls;
			this.IsDataTableCompatible = dataTableCompatible;
			this.IsBatchQueryCompatible = batchQueryCompatible;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000C15F File Offset: 0x0000A35F
		public string Name { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000C167 File Offset: 0x0000A367
		public string BackingFunctionName { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000C16F File Offset: 0x0000A36F
		public FunctionCategory FunctionCategory { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000C177 File Offset: 0x0000A377
		public bool CanBeHandledByProcessing { get; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000C17F File Offset: 0x0000A37F
		public bool CanBeHandledByQuery { get; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000C187 File Offset: 0x0000A387
		public bool IsBatchQueryCompatible { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000C18F File Offset: 0x0000A38F
		public bool IgnoresNulls { get; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000C197 File Offset: 0x0000A397
		public ReadOnlyCollection<ArgumentDescriptor> Arguments { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000C19F File Offset: 0x0000A39F
		public bool ReturnsNulls { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
		public bool IsDataTableCompatible { get; }

		// Token: 0x060005FF RID: 1535 RVA: 0x0000C1AF File Offset: 0x0000A3AF
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FunctionDescriptor);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public bool Equals(FunctionDescriptor other)
		{
			bool flag;
			if (FunctionDescriptor.CheckReferenceAndTypeEquality(this, other, out flag))
			{
				return flag;
			}
			return this.BackingFunctionName == other.BackingFunctionName && this.CanBeHandledByProcessing == other.CanBeHandledByProcessing && this.CanBeHandledByQuery == other.CanBeHandledByQuery && this.IgnoresNulls == other.IgnoresNulls && this.ReturnsNulls == other.ReturnsNulls && this.Arguments.SequenceEqual(other.Arguments) && this.IsDataTableCompatible == other.IsDataTableCompatible && this.IsBatchQueryCompatible == other.IsBatchQueryCompatible;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000C262 File Offset: 0x0000A462
		private static bool CheckReferenceAndTypeEquality(FunctionDescriptor @this, FunctionDescriptor other, out bool areEqual)
		{
			if (@this == other)
			{
				areEqual = true;
				return true;
			}
			if (@this == null || other == null || @this.FunctionCategory != other.FunctionCategory || !FunctionDescriptorFactory.FunctionNameComparer.Equals(@this.Name, other.Name))
			{
				areEqual = false;
				return true;
			}
			areEqual = false;
			return false;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		private bool IsValidOptionalArguments(IEnumerable<ArgumentDescriptor> arguments)
		{
			bool flag = true & arguments.SkipWhile((ArgumentDescriptor a) => !a.IsOptional).All((ArgumentDescriptor a) => a.IsOptional);
			bool flag2;
			if (arguments.Any((ArgumentDescriptor a) => a.IsOptional))
			{
				flag2 = !arguments.Any((ArgumentDescriptor a) => a.IsVarArg);
			}
			else
			{
				flag2 = true;
			}
			return flag && flag2;
		}
	}
}
