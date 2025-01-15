using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C7 RID: 1223
	public class DispatchByBodyElementBehavior : IContractBehavior
	{
		// Token: 0x0600254F RID: 9551 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x00084AB2 File Offset: 0x00082CB2
		public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
			dispatchRuntime.OperationSelector = new DispatchByBodyElementOperationSelector(DispatchByBodyElementBehavior.GetSelectorDictionary(contractDescription.Operations));
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x00084ACC File Offset: 0x00082CCC
		private static Dictionary<string, string> GetSelectorDictionary(IEnumerable<OperationDescription> operationDescriptionCollection)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (OperationDescription operationDescription in operationDescriptionCollection)
			{
				IEnumerable<CustomAttributeData> dispatchBodyElementAttribute = DispatchByBodyElementBehavior.GetDispatchBodyElementAttribute(operationDescription);
				if (dispatchBodyElementAttribute != null)
				{
					if (dispatchBodyElementAttribute.Count<CustomAttributeData>() > 1)
					{
						throw new ArgumentException("DispatchByBodyElementAttribute cannot appear more than once on each method");
					}
					try
					{
						dictionary.Add(dispatchBodyElementAttribute.First<CustomAttributeData>().ConstructorArguments[0].Value.ToString(), operationDescription.Name);
					}
					catch (ArgumentException ex)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Body element '{0}' cannot be associated with more than one method", new object[] { dispatchBodyElementAttribute.First<CustomAttributeData>().ConstructorArguments[0].Value }), ex);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x00084BB0 File Offset: 0x00082DB0
		private static IEnumerable<CustomAttributeData> GetDispatchBodyElementAttribute(OperationDescription operationDescription)
		{
			IList<CustomAttributeData> list = null;
			if (operationDescription.BeginMethod != null)
			{
				list = CustomAttributeData.GetCustomAttributes(operationDescription.BeginMethod);
			}
			else if (operationDescription.EndMethod != null)
			{
				list = CustomAttributeData.GetCustomAttributes(operationDescription.EndMethod);
			}
			else if (operationDescription.SyncMethod != null)
			{
				list = CustomAttributeData.GetCustomAttributes(operationDescription.SyncMethod);
			}
			return list.Where((CustomAttributeData a) => a.Constructor.DeclaringType.FullName.Equals(typeof(DispatchByBodyElementAttribute).FullName));
		}
	}
}
