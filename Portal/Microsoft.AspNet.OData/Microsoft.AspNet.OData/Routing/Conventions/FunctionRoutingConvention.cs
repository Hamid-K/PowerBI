using System;
using System.Linq;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000094 RID: 148
	public class FunctionRoutingConvention : NavigationSourceRoutingConvention
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00011156 File Offset: 0x0000F356
		public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			NavigationSourceRoutingConvention.ValidateSelectActionParameters(odataPath, controllerContext, actionMap);
			return FunctionRoutingConvention.SelectActionImpl(odataPath, new WebApiControllerContext(controllerContext, NavigationSourceRoutingConvention.GetControllerResult(controllerContext)), new WebApiActionMap(actionMap));
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00011178 File Offset: 0x0000F378
		internal static string SelectActionImpl(ODataPath odataPath, IWebApiControllerContext controllerContext, IWebApiActionMap actionMap)
		{
			if (controllerContext.Request.Method == ODataRequestMethod.Get)
			{
				string text = null;
				OperationSegment operationSegment = null;
				string pathTemplate = odataPath.PathTemplate;
				if (pathTemplate != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(pathTemplate);
					if (num <= 1036241218U)
					{
						if (num <= 403130866U)
						{
							if (num != 123895828U)
							{
								if (num != 254823474U)
								{
									if (num != 403130866U)
									{
										goto IL_02FA;
									}
									if (!(pathTemplate == "~/entityset/function/$count"))
									{
										goto IL_02FA;
									}
								}
								else if (!(pathTemplate == "~/entityset/cast/function/$count"))
								{
									goto IL_02FA;
								}
								operationSegment = odataPath.Segments[odataPath.Segments.Count - 2] as OperationSegment;
								text = FunctionRoutingConvention.GetFunction(operationSegment).SelectAction(actionMap, true);
								goto IL_02FA;
							}
							if (!(pathTemplate == "~/singleton/function"))
							{
								goto IL_02FA;
							}
						}
						else if (num != 423386854U)
						{
							if (num != 469168524U)
							{
								if (num != 1036241218U)
								{
									goto IL_02FA;
								}
								if (!(pathTemplate == "~/entityset/key/cast/function/$count"))
								{
									goto IL_02FA;
								}
								goto IL_020B;
							}
							else if (!(pathTemplate == "~/singleton/cast/function"))
							{
								goto IL_02FA;
							}
						}
						else
						{
							if (!(pathTemplate == "~/singleton/function/$count"))
							{
								goto IL_02FA;
							}
							goto IL_02CE;
						}
						operationSegment = odataPath.Segments.Last<ODataPathSegment>() as OperationSegment;
						text = FunctionRoutingConvention.GetFunction(operationSegment).SelectAction(actionMap, false);
						goto IL_02FA;
					}
					if (num <= 2372217304U)
					{
						if (num != 1453964642U)
						{
							if (num != 1524636776U)
							{
								if (num != 2372217304U)
								{
									goto IL_02FA;
								}
								if (!(pathTemplate == "~/entityset/key/cast/function"))
								{
									goto IL_02FA;
								}
							}
							else
							{
								if (!(pathTemplate == "~/entityset/cast/function"))
								{
									goto IL_02FA;
								}
								goto IL_025E;
							}
						}
						else
						{
							if (!(pathTemplate == "~/entityset/key/function/$count"))
							{
								goto IL_02FA;
							}
							goto IL_020B;
						}
					}
					else if (num != 2705806654U)
					{
						if (num != 3296036392U)
						{
							if (num != 3813814904U)
							{
								goto IL_02FA;
							}
							if (!(pathTemplate == "~/entityset/key/function"))
							{
								goto IL_02FA;
							}
						}
						else
						{
							if (!(pathTemplate == "~/entityset/function"))
							{
								goto IL_02FA;
							}
							goto IL_025E;
						}
					}
					else
					{
						if (!(pathTemplate == "~/singleton/cast/function/$count"))
						{
							goto IL_02FA;
						}
						goto IL_02CE;
					}
					operationSegment = odataPath.Segments.Last<ODataPathSegment>() as OperationSegment;
					text = FunctionRoutingConvention.GetFunction(operationSegment).SelectAction(actionMap, false);
					if (text != null)
					{
						controllerContext.AddKeyValueToRouteData((KeySegment)odataPath.Segments[1], "key");
						goto IL_02FA;
					}
					goto IL_02FA;
					IL_025E:
					operationSegment = odataPath.Segments.Last<ODataPathSegment>() as OperationSegment;
					text = FunctionRoutingConvention.GetFunction(operationSegment).SelectAction(actionMap, true);
					goto IL_02FA;
					IL_020B:
					operationSegment = odataPath.Segments[odataPath.Segments.Count - 2] as OperationSegment;
					text = FunctionRoutingConvention.GetFunction(operationSegment).SelectAction(actionMap, false);
					if (text != null)
					{
						controllerContext.AddKeyValueToRouteData((KeySegment)odataPath.Segments[1], "key");
						goto IL_02FA;
					}
					goto IL_02FA;
					IL_02CE:
					operationSegment = odataPath.Segments[odataPath.Segments.Count - 2] as OperationSegment;
					text = FunctionRoutingConvention.GetFunction(operationSegment).SelectAction(actionMap, false);
				}
				IL_02FA:
				if (text != null)
				{
					controllerContext.AddFunctionParameterToRouteData(operationSegment);
					return text;
				}
			}
			return null;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001148C File Offset: 0x0000F68C
		private static IEdmFunction GetFunction(OperationSegment segment)
		{
			if (segment != null)
			{
				return segment.Operations.First<IEdmOperation>() as IEdmFunction;
			}
			return null;
		}
	}
}
