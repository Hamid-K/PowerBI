using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python
{
	// Token: 0x02001DD0 RID: 7632
	public class TransformationTextBasicFunctionInliner : BasicFunctionInliner
	{
		// Token: 0x0600FFF0 RID: 65520 RVA: 0x0036FEBA File Offset: 0x0036E0BA
		private TransformationTextBasicFunctionInliner()
		{
		}

		// Token: 0x0600FFF1 RID: 65521 RVA: 0x0036FEC4 File Offset: 0x0036E0C4
		protected override Dictionary<string, IGeneratedFunction> ModifyBinding(Dictionary<string, IGeneratedFunction> newBinding, IReadOnlyDictionary<string, IGeneratedFunction> boundFunctions)
		{
			Dictionary<string, IGeneratedFunction> dictionary = new Dictionary<string, IGeneratedFunction>();
			foreach (KeyValuePair<string, IGeneratedFunction> keyValuePair in newBinding)
			{
				string key = keyValuePair.Key;
				IGeneratedFunction value = keyValuePair.Value;
				if (value != null)
				{
					IGeneratedFunction generatedFunction = boundFunctions[key];
					if (!(value is ITransformationTextGeneratedFunction))
					{
						ITransformationTextGeneratedFunction transformationTextGeneratedFunction = generatedFunction as ITransformationTextGeneratedFunction;
						if (transformationTextGeneratedFunction != null)
						{
							OpaqueGeneratedFunction opaqueGeneratedFunction = value as OpaqueGeneratedFunction;
							ITransformationTextGeneratedFunction transformationTextGeneratedFunction2;
							if (opaqueGeneratedFunction != null)
							{
								transformationTextGeneratedFunction2 = new TransformationTextOpaqueGeneratedFunction(opaqueGeneratedFunction.Parameters, opaqueGeneratedFunction.ReturnType, transformationTextGeneratedFunction.UsedColumns, opaqueGeneratedFunction.StaticCode, opaqueGeneratedFunction.DynamicCode);
							}
							else
							{
								PythonGeneratedFunction pythonGeneratedFunction = value as PythonGeneratedFunction;
								if (pythonGeneratedFunction == null)
								{
									throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Attempted to rename \"{0}\", which was a TTextGeneratedFunction before,", new object[] { key })) + FormattableString.Invariant(FormattableStringFactory.Create("to a function that is neither Opaque nor a PythonGeneratedFunction", Array.Empty<object>())));
								}
								transformationTextGeneratedFunction2 = new TransformationTextPythonGeneratedFunction(pythonGeneratedFunction.Parameters, pythonGeneratedFunction.ReturnType, transformationTextGeneratedFunction.UsedColumns, pythonGeneratedFunction.SSASequence, null, null);
							}
							dictionary[key] = transformationTextGeneratedFunction2;
						}
					}
				}
			}
			foreach (KeyValuePair<string, IGeneratedFunction> keyValuePair2 in dictionary)
			{
				newBinding[keyValuePair2.Key] = keyValuePair2.Value;
			}
			return newBinding;
		}

		// Token: 0x04006039 RID: 24633
		public new static TransformationTextBasicFunctionInliner Instance = new TransformationTextBasicFunctionInliner();
	}
}
