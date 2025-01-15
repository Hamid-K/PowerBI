using System;
using System.Collections;
using System.Text;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Utils;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200002F RID: 47
	internal sealed class TernaryConditionalExpressionVisitor : VisitorBase, IExpressionSyntaxVisitor<TernaryConditionalExpressionSyntax>
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x000049E8 File Offset: 0x00002BE8
		public ExpressionEvaluationResult Evaluate(IExpressionVisitorHost host, TernaryConditionalExpressionSyntax node)
		{
			this.Validate(host, node);
			if (VBConvert.ConvertToBoolean(host.Evaluate(node.Condition).Value))
			{
				return this.EvaluateBranchExpression(host, node.WhenTrue, node.WhenFalse);
			}
			return this.EvaluateBranchExpression(host, node.WhenFalse, node.WhenTrue);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004A3F File Offset: 0x00002C3F
		public void Validate(IExpressionVisitorHost host, TernaryConditionalExpressionSyntax node)
		{
			base.CheckDiagnostics(node);
			host.Validate(node.Condition);
			host.Validate(node.WhenTrue);
			host.Validate(node.WhenFalse);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004A6C File Offset: 0x00002C6C
		public ExpressionAnalysisResult Analyze(IExpressionVisitorHost host, TernaryConditionalExpressionSyntax node)
		{
			ExpressionAnalysisResult expressionAnalysisResult = host.Analyze(node.WhenTrue);
			ExpressionAnalysisResult expressionAnalysisResult2 = host.Analyze(node.WhenFalse);
			return new ExpressionAnalysisResult(expressionAnalysisResult.ContainsObjectReferences || expressionAnalysisResult2.ContainsObjectReferences);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004AAB File Offset: 0x00002CAB
		public bool IsEnabled(TernaryConditionalExpressionSyntax node)
		{
			return true;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004AB0 File Offset: 0x00002CB0
		private ExpressionEvaluationResult EvaluateBranchExpression(IExpressionVisitorHost host, ExpressionSyntax branchExpression, ExpressionSyntax otherExpression)
		{
			ValueTuple<ExpressionEvaluationResult, ExpressionEvaluationDetails> valueTuple = host.EvaluateWithDetails(branchExpression);
			if (!valueTuple.Item2.TypeAlignmentInvalidated && !host.Analyze(branchExpression).ContainsObjectReferences)
			{
				object obj;
				if (this.TryCheckForTypeAlignment(host, valueTuple.Item1, otherExpression, out obj))
				{
					return new ExpressionEvaluationResult(obj);
				}
				host.InvalidateTypeAlignment();
			}
			return new ExpressionEvaluationResult(valueTuple.Item1.Value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004B14 File Offset: 0x00002D14
		private bool TryCheckForTypeAlignment(IExpressionVisitorHost host, ExpressionEvaluationResult result, ExpressionSyntax otherExpression, out object alignedResult)
		{
			alignedResult = null;
			ExpressionEvaluationResult item;
			try
			{
				if (host.Analyze(otherExpression).ContainsObjectReferences)
				{
					return false;
				}
				ValueTuple<ExpressionEvaluationResult, ExpressionEvaluationDetails> valueTuple = host.EvaluateWithDetails(otherExpression);
				if (valueTuple.Item2.TypeAlignmentInvalidated)
				{
					return false;
				}
				item = valueTuple.Item1;
			}
			catch
			{
				return false;
			}
			if (result.Value == null)
			{
				if (item.Type.IsValueType)
				{
					alignedResult = Activator.CreateInstance(item.Type);
					return true;
				}
			}
			else
			{
				TypeCode typeCode = Type.GetTypeCode(result.Type);
				TypeCode typeCode2 = Type.GetTypeCode(item.Type);
				if (typeCode == typeCode2)
				{
					alignedResult = result.Value;
					return true;
				}
				if (TypeUtils.IsNumeric(typeCode) && TypeUtils.IsNumeric(typeCode2))
				{
					int num = this.TypeCodeToDominantNumericTypeIndex(typeCode);
					int num2 = this.TypeCodeToDominantNumericTypeIndex(typeCode2);
					TypeCode typeCode3 = (TypeCode)this._dominantNumericType[num, num2];
					alignedResult = Convert.ChangeType(result.Value, typeCode3);
					return true;
				}
				bool flag;
				if (this.IsText(typeCode, result.Value, out flag) && typeCode2 == TypeCode.String)
				{
					if (typeCode == TypeCode.Char)
					{
						alignedResult = Convert.ChangeType(result.Value, typeCode2);
						return true;
					}
					if (flag)
					{
						alignedResult = this.ConvertToString((IEnumerable)result.Value);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004C70 File Offset: 0x00002E70
		private bool IsText(TypeCode typeCode, object value, out bool isCharArray)
		{
			isCharArray = false;
			if (typeCode != TypeCode.Object)
			{
				return typeCode == TypeCode.Char || typeCode == TypeCode.String;
			}
			isCharArray = this.IsCharArray(value);
			return isCharArray;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004C94 File Offset: 0x00002E94
		private bool IsCharArray(object value)
		{
			if (value.GetType().IsArray)
			{
				using (IEnumerator enumerator = ((IEnumerable)value).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!(enumerator.Current is char))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004D00 File Offset: 0x00002F00
		private string ConvertToString(IEnumerable values)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in values)
			{
				stringBuilder.Append(obj.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004D64 File Offset: 0x00002F64
		private int TypeCodeToDominantNumericTypeIndex(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.SByte:
				return 1;
			case TypeCode.Byte:
				return 0;
			case TypeCode.Int16:
				return 2;
			case TypeCode.UInt16:
				return 3;
			case TypeCode.Int32:
				return 4;
			case TypeCode.UInt32:
				return 5;
			case TypeCode.Int64:
				return 6;
			case TypeCode.UInt64:
				return 7;
			case TypeCode.Single:
				return 8;
			case TypeCode.Double:
				return 9;
			case TypeCode.Decimal:
				return 10;
			default:
				throw new ArgumentException(string.Format("Unsupported TypeCode {0}", typeCode));
			}
		}

		// Token: 0x04000043 RID: 67
		private const byte _____Byte = 6;

		// Token: 0x04000044 RID: 68
		private const byte ____SByte = 5;

		// Token: 0x04000045 RID: 69
		private const byte ____Int16 = 7;

		// Token: 0x04000046 RID: 70
		private const byte ___UInt16 = 8;

		// Token: 0x04000047 RID: 71
		private const byte ____Int32 = 9;

		// Token: 0x04000048 RID: 72
		private const byte ___UInt32 = 10;

		// Token: 0x04000049 RID: 73
		private const byte ____Int64 = 11;

		// Token: 0x0400004A RID: 74
		private const byte ___UInt64 = 12;

		// Token: 0x0400004B RID: 75
		private const byte ___Single = 13;

		// Token: 0x0400004C RID: 76
		private const byte ___Double = 14;

		// Token: 0x0400004D RID: 77
		private const byte __Decimal = 15;

		// Token: 0x0400004E RID: 78
		private const byte ___Object = 1;

		// Token: 0x0400004F RID: 79
		private readonly byte[,] _dominantNumericType = new byte[,]
		{
			{
				6, 1, 7, 8, 9, 10, 11, 12, 13, 14,
				15
			},
			{
				1, 5, 7, 1, 9, 1, 11, 1, 13, 14,
				15
			},
			{
				7, 7, 7, 1, 9, 1, 11, 1, 13, 14,
				15
			},
			{
				8, 1, 1, 8, 9, 10, 11, 12, 13, 14,
				15
			},
			{
				9, 9, 9, 9, 9, 1, 11, 1, 13, 14,
				15
			},
			{
				10, 1, 1, 10, 1, 10, 11, 12, 13, 14,
				15
			},
			{
				11, 11, 11, 11, 11, 11, 11, 1, 13, 14,
				15
			},
			{
				12, 1, 1, 12, 1, 12, 1, 12, 13, 14,
				15
			},
			{
				13, 13, 13, 13, 13, 13, 13, 13, 13, 14,
				13
			},
			{
				14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
				14
			},
			{
				15, 15, 15, 15, 15, 15, 15, 15, 13, 14,
				15
			}
		};
	}
}
