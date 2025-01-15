using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004E6 RID: 1254
	internal class ContractObjectModel
	{
		// Token: 0x06002611 RID: 9745 RVA: 0x0008739C File Offset: 0x0008559C
		public ContractObjectModel(Type contractType, ProxyGenerationOptions proxyGenerationOptions)
		{
			this.ServiceContractName = string.Empty;
			this.ServiceContractNamespace = string.Empty;
			this.Contract = contractType;
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(contractType);
			CustomAttributeData customAttribute = ExtendedReflection.GetCustomAttribute(customAttributes, typeof(ServiceContractAttribute).FullName);
			ExtendedDiagnostics.EnsureArgumentNotNull<CustomAttributeData>(customAttribute, "Contract must contain the 'ServiceContract' attribute");
			object parameterValueFromCustomAttribute = ExtendedReflection.GetParameterValueFromCustomAttribute(customAttribute, "Name");
			this.ServiceContractName = ((parameterValueFromCustomAttribute != null) ? parameterValueFromCustomAttribute.ToString() : string.Empty);
			object parameterValueFromCustomAttribute2 = ExtendedReflection.GetParameterValueFromCustomAttribute(customAttribute, "Namespace");
			this.ServiceContractNamespace = ((parameterValueFromCustomAttribute2 != null) ? parameterValueFromCustomAttribute2.ToString() : string.Empty);
			ExtendedDiagnostics.EnsureArgumentNotNull<CustomAttributeData>(customAttribute, "Contract must contain the 'ServiceContract' attribute");
			CustomAttributeData customAttribute2 = ExtendedReflection.GetCustomAttribute(customAttributes, typeof(ECFContractAttribute).FullName);
			object obj = ((customAttribute2 != null) ? ExtendedReflection.GetParameterValueFromCustomAttribute(customAttribute2, "FlattenHierarchy") : null);
			this.FlattenHierarchy = obj != null && (bool)obj;
			this.ParseContract();
			if (this.Methods.Any((ContractMethod method) => method.RemovableParameters.Count > 0 || method.UseExtendedResult) && !proxyGenerationOptions.HasFlag(ProxyGenerationOptions.IgnoreRemovableParams))
			{
				this.CreateInternalInterface = true;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x000874C5 File Offset: 0x000856C5
		// (set) Token: 0x06002613 RID: 9747 RVA: 0x000874CD File Offset: 0x000856CD
		internal Type Contract { get; private set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000874D6 File Offset: 0x000856D6
		// (set) Token: 0x06002615 RID: 9749 RVA: 0x000874DE File Offset: 0x000856DE
		public string FullName { get; private set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x000874E7 File Offset: 0x000856E7
		// (set) Token: 0x06002617 RID: 9751 RVA: 0x000874EF File Offset: 0x000856EF
		public string ServiceContractName { get; private set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x000874F8 File Offset: 0x000856F8
		// (set) Token: 0x06002619 RID: 9753 RVA: 0x00087500 File Offset: 0x00085700
		public string ServiceContractNamespace { get; private set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x00087509 File Offset: 0x00085709
		// (set) Token: 0x0600261B RID: 9755 RVA: 0x00087511 File Offset: 0x00085711
		public string GeneratedName { get; private set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x0008751A File Offset: 0x0008571A
		// (set) Token: 0x0600261D RID: 9757 RVA: 0x00087522 File Offset: 0x00085722
		public string Namespace { get; private set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x0008752B File Offset: 0x0008572B
		// (set) Token: 0x0600261F RID: 9759 RVA: 0x00087533 File Offset: 0x00085733
		public string Name { get; private set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x0008753C File Offset: 0x0008573C
		// (set) Token: 0x06002621 RID: 9761 RVA: 0x00087544 File Offset: 0x00085744
		public ICollection<ContractMethod> Methods { get; private set; }

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x0008754D File Offset: 0x0008574D
		// (set) Token: 0x06002623 RID: 9763 RVA: 0x00087555 File Offset: 0x00085755
		public bool CreateInternalInterface { get; set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x0008755E File Offset: 0x0008575E
		// (set) Token: 0x06002625 RID: 9765 RVA: 0x00087566 File Offset: 0x00085766
		public bool FlattenHierarchy { get; set; }

		// Token: 0x06002626 RID: 9766 RVA: 0x0008756F File Offset: 0x0008576F
		private void ParseContract()
		{
			this.GetName();
			this.GetNamespace();
			this.GetMethods();
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x00087584 File Offset: 0x00085784
		private void GetName()
		{
			this.FullName = this.Contract.FullName;
			this.FullName = this.FullName.Replace("+", ".");
			this.Name = this.Contract.Name;
			string text = this.Contract.Name;
			if (this.Contract.Name[0].Equals('I'))
			{
				text = text.Substring(1, text.Length - 1);
			}
			this.GeneratedName = text;
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x0008760E File Offset: 0x0008580E
		private void GetNamespace()
		{
			this.Namespace = this.Contract.Namespace;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x00087624 File Offset: 0x00085824
		private void GetMethods()
		{
			this.Methods = new List<ContractMethod>();
			foreach (MethodInfo methodInfo in this.GetContractMethods())
			{
				this.Methods.Add(new ContractMethod(this, methodInfo));
			}
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00087688 File Offset: 0x00085888
		public static ContractMethod GetEndContractMethod(ContractMethod contractMethod, ContractObjectModel com)
		{
			ContractMethod contractMethod2 = com.Methods.Where((ContractMethod method) => method.Method.Name.Substring(3, method.Method.Name.Length - 3) == contractMethod.Method.Name.Substring(5, contractMethod.Method.Name.Length - 5)).FirstOrDefault<ContractMethod>();
			if (contractMethod2 != null)
			{
				return contractMethod2;
			}
			throw new ArgumentException();
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000876CC File Offset: 0x000858CC
		internal IEnumerable<MethodInfo> GetContractMethods()
		{
			List<MethodInfo> list = new List<MethodInfo>(this.Contract.GetMethods());
			if (this.FlattenHierarchy)
			{
				ExtendedDiagnostics.EnsureOperation(this.Contract.IsInterface, "Cannot flatten because contract '{0}' is not an interface".FormatWithInvariantCulture(new object[] { this.Contract.FullName }));
				foreach (Type type in this.Contract.GetInterfaces())
				{
					list.AddRange(type.GetMethods());
				}
			}
			return list;
		}
	}
}
