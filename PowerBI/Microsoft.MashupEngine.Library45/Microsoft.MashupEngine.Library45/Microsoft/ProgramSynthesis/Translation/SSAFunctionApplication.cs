using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002E8 RID: 744
	public class SSAFunctionApplication : SSARValue
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0002E4CD File Offset: 0x0002C6CD
		public IReadOnlyList<SSAValue> FunctionArguments
		{
			get
			{
				return this._functionArguments;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x0002E4D5 File Offset: 0x0002C6D5
		public string FunctionName
		{
			get
			{
				return base.LanguageSpecificString;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0002E4DD File Offset: 0x0002C6DD
		public bool IsFunctionLocal { get; }

		// Token: 0x06001013 RID: 4115 RVA: 0x0002E4E5 File Offset: 0x0002C6E5
		public SSAFunctionApplication(Type valueType, string functionName, params SSAValue[] arguments)
			: this(valueType, functionName, arguments, false)
		{
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0002E4F4 File Offset: 0x0002C6F4
		public SSAFunctionApplication(Type valueType, string functionName, IEnumerable<SSAValue> arguments, bool isFunctionLocal)
			: base(valueType, functionName)
		{
			this._functionArguments = arguments.ToList<SSAValue>();
			this.IsFunctionLocal = isFunctionLocal;
			foreach (SSAValue ssavalue in this._functionArguments)
			{
				ssavalue.ImmediateDownLinks.Add(this);
				base.ImmediateUpLinks.Add(ssavalue);
			}
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0002E578 File Offset: 0x0002C778
		public bool Equals(SSAFunctionApplication other)
		{
			return other == this || (other != null && (this.FunctionName == other.FunctionName && this.FunctionArguments.SequenceEqual(other.FunctionArguments)) && base.ValueType == other.ValueType);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0002E5C9 File Offset: 0x0002C7C9
		public override bool Equals(SSAValue other)
		{
			return other == this || (other != null && this.Equals(other as SSAFunctionApplication));
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0002E5E4 File Offset: 0x0002C7E4
		public override int GetHashCode()
		{
			if (this._hashCode == null)
			{
				this._hashCode = new int?((((this.FunctionName.GetHashCode() * 18859) ^ this.FunctionArguments.OrderDependentHashCode<SSAValue>()) * 19571) ^ base.ValueType.GetHashCode());
			}
			return this._hashCode.Value;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0002E644 File Offset: 0x0002C844
		public override string ToString()
		{
			string text = "{0}({1})";
			object[] array = new object[2];
			array[0] = this.FunctionName;
			array[1] = string.Join(", ", this.FunctionArguments.Select((SSAValue arg) => arg.ToString()));
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0002E6A6 File Offset: 0x0002C8A6
		public override IEnumerable<SSAValue> AllDependencies
		{
			get
			{
				return base.ImmediateUpLinks.Concat(this.FunctionArguments).SelectMany((SSAValue dep) => dep.AllDependencies.PrependItem(dep));
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0002E6E0 File Offset: 0x0002C8E0
		public void SubstituteArgument(SSAValue argument, SSAValue substitution)
		{
			List<int> list = this._functionArguments.FindAllIndexes((SSAValue arg) => arg.Equals(argument), 0).ToList<int>();
			if (list.Count == 0)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not find argument to be substituted for in call to {0}.{1}", new object[]
				{
					base.GetType(),
					"SubstituteArgument"
				})));
			}
			foreach (int num in list)
			{
				this._functionArguments[num] = substitution;
				this._functionArguments[num].ImmediateDownLinks.Remove(this);
			}
			base.ImmediateUpLinks.Remove(argument);
			base.ImmediateUpLinks.Add(substitution);
			substitution.ImmediateDownLinks.Add(this);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0002E7D8 File Offset: 0x0002C9D8
		public override SSARValue Substitute(SSARegister register, SSAValue newValue)
		{
			return new SSAFunctionApplication(base.ValueType, this.FunctionName, this.SubstituteFunctionArguments(register, newValue), this.IsFunctionLocal);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0002E7F9 File Offset: 0x0002C9F9
		public override void SubstituteInPlace(SSARegister register, SSAValue newValue)
		{
			base.SubstituteInPlace(register, newValue);
			this._functionArguments = this.SubstituteFunctionArguments(register, newValue).ToList<SSAValue>();
			this._hashCode = null;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0002E824 File Offset: 0x0002CA24
		private IEnumerable<SSAValue> SubstituteFunctionArguments(SSARegister register, SSAValue newValue)
		{
			return this.FunctionArguments.Select(delegate(SSAValue arg)
			{
				if (arg.Equals(register))
				{
					return newValue;
				}
				SSARValue ssarvalue = arg as SSARValue;
				if (ssarvalue != null)
				{
					ssarvalue.SubstituteInPlace(register, newValue);
				}
				return arg;
			}).ToList<SSAValue>();
		}

		// Token: 0x040007D0 RID: 2000
		private List<SSAValue> _functionArguments;

		// Token: 0x040007D1 RID: 2001
		private int? _hashCode;
	}
}
