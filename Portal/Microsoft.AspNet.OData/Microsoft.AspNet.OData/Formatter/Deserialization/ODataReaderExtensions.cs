using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001B1 RID: 433
	public static class ODataReaderExtensions
	{
		// Token: 0x06000E61 RID: 3681 RVA: 0x0003AAE8 File Offset: 0x00038CE8
		public static ODataItemBase ReadResourceOrResourceSet(this ODataReader reader)
		{
			if (reader == null)
			{
				throw Error.ArgumentNull("reader");
			}
			ODataItemBase odataItemBase = null;
			Stack<ODataItemBase> stack = new Stack<ODataItemBase>();
			while (reader.Read())
			{
				switch (reader.State)
				{
				case ODataReaderState.ResourceSetStart:
				{
					ODataResourceSetWrapper odataResourceSetWrapper = new ODataResourceSetWrapper((ODataResourceSet)reader.Item);
					if (stack.Count > 0)
					{
						((ODataNestedResourceInfoWrapper)stack.Peek()).NestedItems.Add(odataResourceSetWrapper);
					}
					else
					{
						odataItemBase = odataResourceSetWrapper;
					}
					stack.Push(odataResourceSetWrapper);
					break;
				}
				case ODataReaderState.ResourceSetEnd:
					stack.Pop();
					break;
				case ODataReaderState.ResourceStart:
				{
					ODataResource odataResource = (ODataResource)reader.Item;
					ODataResourceWrapper odataResourceWrapper = null;
					if (odataResource != null)
					{
						odataResourceWrapper = new ODataResourceWrapper(odataResource);
					}
					if (stack.Count == 0)
					{
						odataItemBase = odataResourceWrapper;
					}
					else
					{
						ODataItemBase odataItemBase2 = stack.Peek();
						ODataResourceSetWrapper odataResourceSetWrapper2 = odataItemBase2 as ODataResourceSetWrapper;
						if (odataResourceSetWrapper2 != null)
						{
							odataResourceSetWrapper2.Resources.Add(odataResourceWrapper);
						}
						else
						{
							((ODataNestedResourceInfoWrapper)odataItemBase2).NestedItems.Add(odataResourceWrapper);
						}
					}
					stack.Push(odataResourceWrapper);
					break;
				}
				case ODataReaderState.ResourceEnd:
					stack.Pop();
					break;
				case ODataReaderState.NestedResourceInfoStart:
				{
					ODataNestedResourceInfoWrapper odataNestedResourceInfoWrapper = new ODataNestedResourceInfoWrapper((ODataNestedResourceInfo)reader.Item);
					((ODataResourceWrapper)stack.Peek()).NestedResourceInfos.Add(odataNestedResourceInfoWrapper);
					stack.Push(odataNestedResourceInfoWrapper);
					break;
				}
				case ODataReaderState.NestedResourceInfoEnd:
					stack.Pop();
					break;
				case ODataReaderState.EntityReferenceLink:
				{
					ODataEntityReferenceLinkBase odataEntityReferenceLinkBase = new ODataEntityReferenceLinkBase((ODataEntityReferenceLink)reader.Item);
					((ODataNestedResourceInfoWrapper)stack.Peek()).NestedItems.Add(odataEntityReferenceLinkBase);
					break;
				}
				}
			}
			return odataItemBase;
		}
	}
}
