using System;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x0200000E RID: 14
	internal class VbConstantIdentifierEvaluator : IVisualBasicConstantEvaluator
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00003C5C File Offset: 0x00001E5C
		public object EvaluateConstant(string visualBasicConstant)
		{
			string text = visualBasicConstant.ToUpperInvariant();
			object obj;
			if (Operators.CompareString(text, "VBCRLF", false) != 0)
			{
				if (Operators.CompareString(text, "VBCR", false) != 0)
				{
					if (Operators.CompareString(text, "VBLF", false) != 0)
					{
						if (Operators.CompareString(text, "VBBINARYCOMPARE", false) != 0)
						{
							if (Operators.CompareString(text, "VBTEXTCOMPARE", false) != 0)
							{
								throw new NotSupportedException(string.Format("Visual Basic constant <{0}> is not supported yet.", visualBasicConstant));
							}
							obj = CompareMethod.Text;
						}
						else
						{
							obj = CompareMethod.Binary;
						}
					}
					else
					{
						obj = "\n";
					}
				}
				else
				{
					obj = "\r";
				}
			}
			else
			{
				obj = "\r\n";
			}
			return obj;
		}
	}
}
