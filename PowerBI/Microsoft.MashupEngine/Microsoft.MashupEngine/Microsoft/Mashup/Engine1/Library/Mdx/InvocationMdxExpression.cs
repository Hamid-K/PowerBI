using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009A9 RID: 2473
	internal class InvocationMdxExpression : MdxExpression
	{
		// Token: 0x060046A2 RID: 18082 RVA: 0x000ECEE6 File Offset: 0x000EB0E6
		public InvocationMdxExpression(MdxFunction function, params MdxExpression[] arguments)
		{
			this.function = function;
			this.arguments = arguments;
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsComplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x000ECEFC File Offset: 0x000EB0FC
		public MdxFunction Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x060046A5 RID: 18085 RVA: 0x000ECF04 File Offset: 0x000EB104
		public bool IsMemberFunction
		{
			get
			{
				MdxFunction mdxFunction = this.function;
				return mdxFunction - MdxFunction.CurrentMember <= 9;
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x060046A6 RID: 18086 RVA: 0x000ECF23 File Offset: 0x000EB123
		public MdxExpression[] Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x000ECF2C File Offset: 0x000EB12C
		public bool Equals(InvocationMdxExpression other)
		{
			bool flag = other != null && this.function == other.function && this.arguments.Length == other.arguments.Length;
			int num = 0;
			while (flag && num < this.arguments.Length)
			{
				flag &= this.arguments[num].Equals(other.arguments[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x000ECF8F File Offset: 0x000EB18F
		public override bool Equals(object other)
		{
			return this.Equals(other as InvocationMdxExpression);
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x000ECFA0 File Offset: 0x000EB1A0
		public override int GetHashCode()
		{
			int num = this.function.GetHashCode() + 5011 * this.arguments.Length;
			for (int i = 0; i < this.arguments.Length; i++)
			{
				num += this.arguments[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x000ECFF8 File Offset: 0x000EB1F8
		public override void Write(MdxExpressionWriter writer)
		{
			int num;
			if (this.IsMemberFunction)
			{
				this.Arguments[0].Write(writer);
				writer.Write(".");
				writer.Write(InvocationMdxExpression.ToString(this.Function));
				num = 1;
			}
			else
			{
				writer.Write(InvocationMdxExpression.ToString(this.Function));
				num = 0;
			}
			for (int i = num; i < this.Arguments.Length; i++)
			{
				if (i == num)
				{
					writer.Write("(");
				}
				using (writer.NewScope())
				{
					this.Arguments[i].Write(writer);
					if (i < this.Arguments.Length - 1)
					{
						writer.Write(",");
					}
				}
				if (i == this.arguments.Length - 1)
				{
					writer.Write(")");
				}
			}
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x000ED0D8 File Offset: 0x000EB2D8
		public static string ToString(MdxFunction function)
		{
			switch (function)
			{
			case MdxFunction.Crossjoin:
				return "CROSSJOIN";
			case MdxFunction.Filter:
				return "FILTER";
			case MdxFunction.Subset:
				return "SUBSET";
			case MdxFunction.Intersect:
				return "INTERSECT";
			case MdxFunction.Union:
				return "UNION";
			case MdxFunction.IsEmpty:
				return "ISEMPTY";
			case MdxFunction.Order:
				return "ORDER";
			case MdxFunction.VbaCDate:
				return "vba!cdate";
			case MdxFunction.NonEmpty:
				return "NONEMPTY";
			case MdxFunction.Except:
				return "EXCEPT";
			case MdxFunction.Descendants:
				return "DESCENDANTS";
			case MdxFunction.Distinct:
				return "DISTINCT";
			case MdxFunction.Ancestor:
				return "ANCESTOR";
			case MdxFunction.AddCalculatedMembers:
				return "ADDCALCULATEDMEMBERS";
			case MdxFunction.Generate:
				return "GENERATE";
			case MdxFunction.Aggregate:
				return "AGGREGATE";
			case MdxFunction.Count:
				return "COUNT";
			case MdxFunction.InStr:
				return "INSTR";
			case MdxFunction.InString:
				return "INSTRING";
			case MdxFunction.IIf:
				return "IIF";
			case MdxFunction.IsValid:
				return "ISVALID";
			case MdxFunction.CurrentMember:
				return "CURRENTMEMBER";
			case MdxFunction.Properties:
				return "PROPERTIES";
			case MdxFunction.Parent:
				return "PARENT";
			case MdxFunction.MemberCaption:
				return "MEMBER_CAPTION";
			case MdxFunction.UniqueName:
				return "UNIQUENAME";
			case MdxFunction.MemberName:
				return "MEMBER_NAME";
			case MdxFunction.MemberAlias:
				return "MEMBER_ALIAS";
			case MdxFunction.AllMembers:
				return "ALLMEMBERS";
			case MdxFunction.Members:
				return "MEMBERS";
			case MdxFunction.MemberUniqueName:
				return "MEMBER_UNIQUE_NAME";
			default:
				throw new InvalidOperationException("Unknown MdxFunction: '" + function.ToString() + "'");
			}
		}

		// Token: 0x04002575 RID: 9589
		private readonly MdxFunction function;

		// Token: 0x04002576 RID: 9590
		private readonly MdxExpression[] arguments;
	}
}
