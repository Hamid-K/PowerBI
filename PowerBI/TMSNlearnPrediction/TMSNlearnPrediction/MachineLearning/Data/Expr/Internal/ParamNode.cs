using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001A8 RID: 424
	internal sealed class ParamNode : Node
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x000329A4 File Offset: 0x00030BA4
		public ParamNode(Token tok, string name, int index, ColumnType type)
			: base(tok)
		{
			this.Name = name;
			this.Index = index;
			this.Type = type;
			if (type == null)
			{
				this.ExprType = ExprType.Error;
				return;
			}
			if (type.IsText)
			{
				this.ExprType = ExprType.TX;
				return;
			}
			if (type.IsBool)
			{
				this.ExprType = ExprType.BL;
				return;
			}
			if (type == NumberType.I4)
			{
				this.ExprType = ExprType.I4;
				return;
			}
			if (type == NumberType.I8)
			{
				this.ExprType = ExprType.I8;
				return;
			}
			if (type == NumberType.R4)
			{
				this.ExprType = ExprType.R4;
				return;
			}
			if (type == NumberType.R8)
			{
				this.ExprType = ExprType.R8;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00032A5B File Offset: 0x00030C5B
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.Param;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00032A5E File Offset: 0x00030C5E
		public override ParamNode AsParam
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00032A61 File Offset: 0x00030C61
		public override ParamNode TestParam
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00032A64 File Offset: 0x00030C64
		public override void Accept(NodeVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040004DD RID: 1245
		public readonly string Name;

		// Token: 0x040004DE RID: 1246
		public readonly int Index;

		// Token: 0x040004DF RID: 1247
		public readonly ColumnType Type;

		// Token: 0x040004E0 RID: 1248
		public ExprType ExprType;
	}
}
