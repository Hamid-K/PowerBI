using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Extensions
{
	// Token: 0x020019B9 RID: 6585
	public static class LearnConstraintExtensions
	{
		// Token: 0x0600D6F5 RID: 55029 RVA: 0x002DADE0 File Offset: 0x002D8FE0
		public static TargetLanguage? ToTargetLanguage(this LearnConstraint constraint)
		{
			if (constraint == null)
			{
				return null;
			}
			TargetLanguage? targetLanguage;
			if (!(constraint is CSharpLearnConstraint))
			{
				if (!(constraint is ExcelLearnConstraint))
				{
					if (!(constraint is PowerAutomateLearnConstraint))
					{
						if (!(constraint is PowerFxLearnConstraint))
						{
							if (!(constraint is PowerQueryLearnConstraint))
							{
								if (!(constraint is PythonLearnConstraint))
								{
									if (!(constraint is PandasLearnConstraint))
									{
										if (!(constraint is PySparkLearnConstraint))
										{
											if (!(constraint is SqlLearnConstraint))
											{
												targetLanguage = null;
											}
											else
											{
												targetLanguage = new TargetLanguage?(TargetLanguage.Sql);
											}
										}
										else
										{
											targetLanguage = new TargetLanguage?(TargetLanguage.PySpark);
										}
									}
									else
									{
										targetLanguage = new TargetLanguage?(TargetLanguage.Pandas);
									}
								}
								else
								{
									targetLanguage = new TargetLanguage?(TargetLanguage.Python);
								}
							}
							else
							{
								targetLanguage = new TargetLanguage?(TargetLanguage.PowerQueryM);
							}
						}
						else
						{
							targetLanguage = new TargetLanguage?(TargetLanguage.PowerApps);
						}
					}
					else
					{
						targetLanguage = new TargetLanguage?(TargetLanguage.PowerAutomate);
					}
				}
				else
				{
					targetLanguage = new TargetLanguage?(TargetLanguage.Excel);
				}
			}
			else
			{
				targetLanguage = new TargetLanguage?(TargetLanguage.CSharp);
			}
			return targetLanguage;
		}
	}
}
