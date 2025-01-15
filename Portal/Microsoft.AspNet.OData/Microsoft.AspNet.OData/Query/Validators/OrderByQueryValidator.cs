using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query.Validators
{
	// Token: 0x020000DD RID: 221
	public class OrderByQueryValidator
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0001A8EB File Offset: 0x00018AEB
		public OrderByQueryValidator(DefaultQuerySettings defaultQuerySettings)
		{
			this._defaultQuerySettings = defaultQuerySettings;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001A8FC File Offset: 0x00018AFC
		public virtual void Validate(OrderByQueryOption orderByOption, ODataValidationSettings validationSettings)
		{
			if (orderByOption == null)
			{
				throw Error.ArgumentNull("orderByOption");
			}
			if (validationSettings == null)
			{
				throw Error.ArgumentNull("validationSettings");
			}
			int num = 0;
			for (OrderByClause orderByClause = orderByOption.OrderByClause; orderByClause != null; orderByClause = orderByClause.ThenBy)
			{
				num++;
				if (num > validationSettings.MaxOrderByNodeCount)
				{
					throw new ODataException(Error.Format(SRResources.OrderByNodeCountExceeded, new object[] { validationSettings.MaxOrderByNodeCount }));
				}
			}
			OrderByModelLimitationsValidator orderByModelLimitationsValidator = new OrderByModelLimitationsValidator(orderByOption.Context, this._defaultQuerySettings.EnableOrderBy);
			bool flag = validationSettings.AllowedOrderByProperties.Count > 0;
			foreach (OrderByNode orderByNode in orderByOption.OrderByNodes)
			{
				OrderByPropertyNode orderByPropertyNode = orderByNode as OrderByPropertyNode;
				if (orderByPropertyNode != null)
				{
					string text = orderByPropertyNode.Property.Name;
					bool flag2 = !orderByModelLimitationsValidator.TryValidate(orderByPropertyNode.OrderByClause, flag);
					if (text != null && flag2 && flag)
					{
						if (!OrderByQueryValidator.IsAllowed(validationSettings, text))
						{
							throw new ODataException(Error.Format(SRResources.NotAllowedOrderByProperty, new object[] { text, "AllowedOrderByProperties" }));
						}
					}
					else if (text != null && !OrderByQueryValidator.IsAllowed(validationSettings, text))
					{
						throw new ODataException(Error.Format(SRResources.NotAllowedOrderByProperty, new object[] { text, "AllowedOrderByProperties" }));
					}
				}
				else
				{
					string text = "$it";
					if (!OrderByQueryValidator.IsAllowed(validationSettings, text))
					{
						throw new ODataException(Error.Format(SRResources.NotAllowedOrderByProperty, new object[] { text, "AllowedOrderByProperties" }));
					}
				}
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		internal static OrderByQueryValidator GetOrderByQueryValidator(ODataQueryContext context)
		{
			if (context == null)
			{
				return new OrderByQueryValidator(new DefaultQuerySettings());
			}
			if (context.RequestContainer != null)
			{
				return ServiceProviderServiceExtensions.GetRequiredService<OrderByQueryValidator>(context.RequestContainer);
			}
			return new OrderByQueryValidator(context.DefaultQuerySettings);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001AAD7 File Offset: 0x00018CD7
		private static bool IsAllowed(ODataValidationSettings validationSettings, string propertyName)
		{
			return validationSettings.AllowedOrderByProperties.Count == 0 || validationSettings.AllowedOrderByProperties.Contains(propertyName);
		}

		// Token: 0x04000239 RID: 569
		private readonly DefaultQuerySettings _defaultQuerySettings;
	}
}
