using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.Metadata.Edm;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000246 RID: 582
	internal abstract class FunctionLibrary
	{
		// Token: 0x060019B8 RID: 6584 RVA: 0x00046B9D File Offset: 0x00044D9D
		protected FunctionLibrary(string namespaceName, ItemCollection edmFunctions, IReadOnlyList<EdmOperator> operators)
		{
			this._namespaceName = namespaceName;
			this._functions = this.CreateFunctionMapping(namespaceName, edmFunctions);
			this._operators = this.CreateOperatorMapping(operators);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00046BC8 File Offset: 0x00044DC8
		private IReadOnlyDictionary<string, List<EdmFunction>> CreateFunctionMapping(string namespaceName, ItemCollection edmFunctions)
		{
			ReadOnlyCollection<EdmFunction> items = edmFunctions.GetItems<EdmFunction>();
			Dictionary<string, List<EdmFunction>> dictionary = new Dictionary<string, List<EdmFunction>>(items.Count);
			foreach (EdmFunction edmFunction in items)
			{
				if (!(edmFunction.NamespaceName != namespaceName))
				{
					EdmFunction edmFunction2 = EdmFunction.Create(edmFunction);
					List<EdmFunction> list;
					if (dictionary.TryGetValue(edmFunction.FullName, out list))
					{
						list.Add(edmFunction2);
					}
					else
					{
						list = new List<EdmFunction>();
						list.Add(edmFunction2);
						dictionary.Add(edmFunction.FullName, list);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00046C68 File Offset: 0x00044E68
		private IReadOnlyDictionary<string, List<EdmOperator>> CreateOperatorMapping(IReadOnlyList<EdmOperator> operators)
		{
			Dictionary<string, List<EdmOperator>> dictionary = new Dictionary<string, List<EdmOperator>>(operators.Count);
			foreach (EdmOperator edmOperator in operators)
			{
				List<EdmOperator> list;
				if (dictionary.TryGetValue(edmOperator.Name, out list))
				{
					list.Add(edmOperator);
				}
				else
				{
					dictionary.Add(edmOperator.Name, new List<EdmOperator> { edmOperator });
				}
			}
			return dictionary;
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x00046CE8 File Offset: 0x00044EE8
		internal static FunctionLibrary Core
		{
			get
			{
				return CoreFunctionLibrary.Instance;
			}
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00046CEF File Offset: 0x00044EEF
		internal IReadOnlyList<EdmFunction> GetFunctions(string functionName)
		{
			return this._functions[functionName];
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00046CFD File Offset: 0x00044EFD
		internal IReadOnlyList<EdmOperator> GetOperators(string operatorName)
		{
			return this._operators[operatorName];
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00046D0B File Offset: 0x00044F0B
		internal bool CanInvokeFunction(string functionName, params ConceptualResultType[] argTypes)
		{
			return this.GetFunctions(functionName).CanInvokeFunction(functionName, argTypes);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00046D1B File Offset: 0x00044F1B
		internal bool CanInvokeOperator(string operatorName, params ConceptualResultType[] argTypes)
		{
			return this.GetOperators(operatorName).CanInvokeOperator(operatorName, argTypes);
		}

		// Token: 0x04000E48 RID: 3656
		private readonly string _namespaceName;

		// Token: 0x04000E49 RID: 3657
		private readonly IReadOnlyDictionary<string, List<EdmFunction>> _functions;

		// Token: 0x04000E4A RID: 3658
		private readonly IReadOnlyDictionary<string, List<EdmOperator>> _operators;
	}
}
