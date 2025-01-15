using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B0 RID: 176
	public sealed class FunctionInfo
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x0001F350 File Offset: 0x0001D550
		private static FunctionInfo.FunctionInfoDictionary CreateFunctionInfos()
		{
			FunctionInfo.FunctionInfoDictionary functionInfoDictionary = new FunctionInfo.FunctionInfoDictionary();
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(1, FunctionName.Add, DataType.Integer, true, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(2, FunctionName.Add, DataType.Decimal, true, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(3, FunctionName.Add, DataType.Float, true, new DataType[]
				{
					DataType.Float,
					DataType.Float
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(4, FunctionName.Subtract, DataType.Integer, true, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(5, FunctionName.Subtract, DataType.Decimal, true, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(6, FunctionName.Subtract, DataType.Float, true, new DataType[]
				{
					DataType.Float,
					DataType.Float
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(7, FunctionName.Multiply, DataType.Integer, true, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(8, FunctionName.Multiply, DataType.Decimal, true, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(9, FunctionName.Multiply, DataType.Float, true, new DataType[]
				{
					DataType.Float,
					DataType.Float
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(10, FunctionName.Divide, DataType.Decimal, true, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(11, FunctionName.Divide, DataType.Float, true, new DataType[]
				{
					DataType.Float,
					DataType.Float
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(12, FunctionName.Negate, DataType.Integer, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateScalar(13, FunctionName.Negate, DataType.Decimal, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateScalar(14, FunctionName.Negate, DataType.Float, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(15, FunctionName.Mod, DataType.Integer, true, new DataType[]
			{
				DataType.Integer,
				DataType.Integer
			}) });
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(16, FunctionName.Power, DataType.Integer, true, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(17, FunctionName.Power, DataType.Decimal, true, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(18, FunctionName.Power, DataType.Float, true, new DataType[]
				{
					DataType.Float,
					DataType.Float
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(19, FunctionName.Equals, DataType.Boolean, false, new DataType[2]),
				FunctionInfo.CreateScalar(20, FunctionName.Equals, DataType.Boolean, false, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(21, FunctionName.Equals, DataType.Boolean, false, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(22, FunctionName.Equals, DataType.Boolean, false, new DataType[]
				{
					DataType.Float,
					DataType.Float
				}),
				FunctionInfo.CreateScalar(23, FunctionName.Equals, DataType.Boolean, false, new DataType[]
				{
					DataType.Boolean,
					DataType.Boolean
				}),
				FunctionInfo.CreateScalar(24, FunctionName.Equals, DataType.Boolean, false, new DataType[]
				{
					DataType.DateTime,
					DataType.DateTime
				}),
				FunctionInfo.CreateScalar(200, FunctionName.Equals, DataType.Boolean, false, new DataType[]
				{
					DataType.Time,
					DataType.Time
				}),
				FunctionInfo.CreateScalar(25, FunctionName.Equals, DataType.Boolean, false, new DataType[]
				{
					DataType.EntityKey,
					DataType.EntityKey
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(26, FunctionName.NotEquals, DataType.Boolean, false, new DataType[2]),
				FunctionInfo.CreateScalar(27, FunctionName.NotEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(28, FunctionName.NotEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(29, FunctionName.NotEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Float,
					DataType.Float
				}),
				FunctionInfo.CreateScalar(30, FunctionName.NotEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Boolean,
					DataType.Boolean
				}),
				FunctionInfo.CreateScalar(31, FunctionName.NotEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.DateTime,
					DataType.DateTime
				}),
				FunctionInfo.CreateScalar(201, FunctionName.NotEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Time,
					DataType.Time
				}),
				FunctionInfo.CreateScalar(32, FunctionName.NotEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.EntityKey,
					DataType.EntityKey
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(33, FunctionName.GreaterThan, DataType.Boolean, false, new DataType[2]),
				FunctionInfo.CreateScalar(34, FunctionName.GreaterThan, DataType.Boolean, false, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(35, FunctionName.GreaterThan, DataType.Boolean, false, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(36, FunctionName.GreaterThan, DataType.Boolean, false, new DataType[]
				{
					DataType.Float,
					DataType.Float
				}),
				FunctionInfo.CreateScalar(37, FunctionName.GreaterThan, DataType.Boolean, false, new DataType[]
				{
					DataType.DateTime,
					DataType.DateTime
				}),
				FunctionInfo.CreateScalar(202, FunctionName.GreaterThan, DataType.Boolean, false, new DataType[]
				{
					DataType.Time,
					DataType.Time
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(38, FunctionName.GreaterThanOrEquals, DataType.Boolean, false, new DataType[2]),
				FunctionInfo.CreateScalar(39, FunctionName.GreaterThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(40, FunctionName.GreaterThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(41, FunctionName.GreaterThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Float,
					DataType.Float
				}),
				FunctionInfo.CreateScalar(42, FunctionName.GreaterThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.DateTime,
					DataType.DateTime
				}),
				FunctionInfo.CreateScalar(203, FunctionName.GreaterThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Time,
					DataType.Time
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(43, FunctionName.LessThan, DataType.Boolean, false, new DataType[2]),
				FunctionInfo.CreateScalar(44, FunctionName.LessThan, DataType.Boolean, false, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(45, FunctionName.LessThan, DataType.Boolean, false, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(46, FunctionName.LessThan, DataType.Boolean, false, new DataType[]
				{
					DataType.Float,
					DataType.Float
				}),
				FunctionInfo.CreateScalar(47, FunctionName.LessThan, DataType.Boolean, false, new DataType[]
				{
					DataType.DateTime,
					DataType.DateTime
				}),
				FunctionInfo.CreateScalar(204, FunctionName.LessThan, DataType.Boolean, false, new DataType[]
				{
					DataType.Time,
					DataType.Time
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(48, FunctionName.LessThanOrEquals, DataType.Boolean, false, new DataType[2]),
				FunctionInfo.CreateScalar(49, FunctionName.LessThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(50, FunctionName.LessThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Decimal,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(51, FunctionName.LessThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Float,
					DataType.Float
				}),
				FunctionInfo.CreateScalar(52, FunctionName.LessThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.DateTime,
					DataType.DateTime
				}),
				FunctionInfo.CreateScalar(205, FunctionName.LessThanOrEquals, DataType.Boolean, false, new DataType[]
				{
					DataType.Time,
					DataType.Time
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateIn(53, FunctionName.In, DataType.String),
				FunctionInfo.CreateIn(54, FunctionName.In, DataType.Integer),
				FunctionInfo.CreateIn(55, FunctionName.In, DataType.Decimal),
				FunctionInfo.CreateIn(56, FunctionName.In, DataType.Float),
				FunctionInfo.CreateIn(57, FunctionName.In, DataType.Boolean),
				FunctionInfo.CreateIn(58, FunctionName.In, DataType.DateTime),
				FunctionInfo.CreateIn(206, FunctionName.In, DataType.Time),
				FunctionInfo.CreateIn(59, FunctionName.In, DataType.EntityKey)
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(60, FunctionName.And, DataType.Boolean, false, new DataType[]
			{
				DataType.Boolean,
				DataType.Boolean
			}) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(61, FunctionName.Or, DataType.Boolean, false, new DataType[]
			{
				DataType.Boolean,
				DataType.Boolean
			}) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(62, FunctionName.Not, DataType.Boolean, false, new DataType[] { DataType.Boolean }) });
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(63, FunctionName.Truncate, DataType.Integer, true, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(64, FunctionName.Truncate, DataType.Decimal, true, new DataType[]
				{
					DataType.Decimal,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(65, FunctionName.Truncate, DataType.Float, true, new DataType[]
				{
					DataType.Float,
					DataType.Integer
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(66, FunctionName.Round, DataType.Integer, true, new DataType[]
				{
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(67, FunctionName.Round, DataType.Decimal, true, new DataType[]
				{
					DataType.Decimal,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(68, FunctionName.Round, DataType.Float, true, new DataType[]
				{
					DataType.Float,
					DataType.Integer
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(69, FunctionName.Integer, DataType.Integer, true, new DataType[1]),
				FunctionInfo.CreateScalar(70, FunctionName.Integer, DataType.Integer, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateScalar(71, FunctionName.Integer, DataType.Integer, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateScalar(72, FunctionName.Integer, DataType.Integer, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(73, FunctionName.Decimal, DataType.Decimal, true, new DataType[1]),
				FunctionInfo.CreateScalar(74, FunctionName.Decimal, DataType.Decimal, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateScalar(75, FunctionName.Decimal, DataType.Decimal, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateScalar(76, FunctionName.Decimal, DataType.Decimal, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(77, FunctionName.Float, DataType.Float, true, new DataType[1]),
				FunctionInfo.CreateScalar(78, FunctionName.Float, DataType.Float, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateScalar(79, FunctionName.Float, DataType.Float, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateScalar(80, FunctionName.Float, DataType.Float, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(81, FunctionName.String, DataType.String, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateScalar(82, FunctionName.String, DataType.String, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateScalar(83, FunctionName.String, DataType.String, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(84, FunctionName.Length, DataType.Integer, false, new DataType[1]) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(85, FunctionName.Find, DataType.Integer, false, new DataType[2]) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(86, FunctionName.Substring, DataType.String, true, new DataType[]
			{
				DataType.String,
				DataType.Integer,
				DataType.Integer
			}) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(87, FunctionName.Left, DataType.String, true, new DataType[]
			{
				DataType.String,
				DataType.Integer
			}) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(88, FunctionName.Right, DataType.String, true, new DataType[]
			{
				DataType.String,
				DataType.Integer
			}) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(89, FunctionName.Concat, DataType.String, false, new DataType[2]) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(90, FunctionName.Lower, DataType.String, true, new DataType[1]) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(91, FunctionName.Upper, DataType.String, true, new DataType[1]) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(92, FunctionName.LTrim, DataType.String, true, new DataType[1]) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(93, FunctionName.RTrim, DataType.String, true, new DataType[1]) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(94, FunctionName.Replace, DataType.String, true, new DataType[3]) });
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(95, FunctionName.DateTime, DataType.DateTime, true, new DataType[]
				{
					DataType.Integer,
					DataType.Integer,
					DataType.Integer,
					DataType.Integer,
					DataType.Integer,
					DataType.Decimal
				}),
				FunctionInfo.CreateScalar(96, FunctionName.DateTime, DataType.DateTime, true, new DataType[]
				{
					DataType.Integer,
					DataType.Integer,
					DataType.Integer,
					DataType.Integer,
					DataType.Integer,
					DataType.Float
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(97, FunctionName.Date, DataType.DateTime, true, new DataType[]
				{
					DataType.Integer,
					DataType.Integer,
					DataType.Integer
				}),
				FunctionInfo.CreateScalar(98, FunctionName.Date, DataType.DateTime, true, new DataType[] { DataType.DateTime })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(221, FunctionName.Time, DataType.Time, true, new DataType[] { DataType.DateTime }) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(99, FunctionName.Year, DataType.Integer, true, new DataType[] { DataType.DateTime }) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(100, FunctionName.Quarter, DataType.Integer, true, new DataType[] { DataType.DateTime }) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(101, FunctionName.Month, DataType.Integer, true, new DataType[] { DataType.DateTime }) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(102, FunctionName.Day, DataType.Integer, true, new DataType[] { DataType.DateTime }) });
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(103, FunctionName.Hour, DataType.Integer, true, new DataType[] { DataType.DateTime }),
				FunctionInfo.CreateScalar(216, FunctionName.Hour, DataType.Integer, true, new DataType[] { DataType.Time })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(104, FunctionName.Minute, DataType.Integer, true, new DataType[] { DataType.DateTime }),
				FunctionInfo.CreateScalar(217, FunctionName.Minute, DataType.Integer, true, new DataType[] { DataType.Time })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(105, FunctionName.Second, DataType.Decimal, true, new DataType[] { DataType.DateTime }),
				FunctionInfo.CreateScalar(218, FunctionName.Second, DataType.Decimal, true, new DataType[] { DataType.Time })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(106, FunctionName.DayOfYear, DataType.Integer, true, new DataType[] { DataType.DateTime }) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(107, FunctionName.Week, DataType.Integer, true, new DataType[] { DataType.DateTime }) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalar(108, FunctionName.DayOfWeek, DataType.Integer, true, new DataType[] { DataType.DateTime }) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalarStatic(109, FunctionName.Now, DataType.DateTime, false, Array.Empty<DataType>()) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalarStatic(110, FunctionName.Today, DataType.DateTime, false, Array.Empty<DataType>()) });
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(111, FunctionName.DateDiff, DataType.Integer, true, new DataType[]
				{
					DataType.String,
					DataType.DateTime,
					DataType.DateTime
				}),
				FunctionInfo.CreateScalar(219, FunctionName.DateDiff, DataType.Integer, true, new DataType[]
				{
					DataType.String,
					DataType.Time,
					DataType.Time
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateScalar(112, FunctionName.DateAdd, DataType.DateTime, true, new DataType[]
				{
					DataType.String,
					DataType.Integer,
					DataType.DateTime
				}),
				FunctionInfo.CreateScalar(220, FunctionName.DateAdd, DataType.Time, true, new DataType[]
				{
					DataType.String,
					DataType.Integer,
					DataType.Time
				})
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(113, FunctionName.Sum, true, DataType.Integer, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(114, FunctionName.Sum, true, DataType.Decimal, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(115, FunctionName.Sum, true, DataType.Float, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(116, FunctionName.Avg, false, DataType.Decimal, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(117, FunctionName.Avg, false, DataType.Decimal, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(118, FunctionName.Avg, false, DataType.Float, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(119, FunctionName.Max, true, DataType.String, true, new DataType[1]),
				FunctionInfo.CreateAggregate(120, FunctionName.Max, true, DataType.Integer, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(121, FunctionName.Max, true, DataType.Decimal, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(122, FunctionName.Max, true, DataType.Float, true, new DataType[] { DataType.Float }),
				FunctionInfo.CreateAggregate(123, FunctionName.Max, true, DataType.DateTime, true, new DataType[] { DataType.DateTime }),
				FunctionInfo.CreateAggregate(207, FunctionName.Max, true, DataType.Time, true, new DataType[] { DataType.Time })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(124, FunctionName.Min, true, DataType.String, true, new DataType[1]),
				FunctionInfo.CreateAggregate(125, FunctionName.Min, true, DataType.Integer, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(126, FunctionName.Min, true, DataType.Decimal, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(127, FunctionName.Min, true, DataType.Float, true, new DataType[] { DataType.Float }),
				FunctionInfo.CreateAggregate(128, FunctionName.Min, true, DataType.DateTime, true, new DataType[] { DataType.DateTime }),
				FunctionInfo.CreateAggregate(208, FunctionName.Min, true, DataType.Time, true, new DataType[] { DataType.Time })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(129, FunctionName.Count, false, DataType.Integer, false, new DataType[1]),
				FunctionInfo.CreateAggregate(130, FunctionName.Count, false, DataType.Integer, false, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(131, FunctionName.Count, false, DataType.Integer, false, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(132, FunctionName.Count, false, DataType.Integer, false, new DataType[] { DataType.Float }),
				FunctionInfo.CreateAggregate(133, FunctionName.Count, false, DataType.Integer, false, new DataType[] { DataType.Boolean }),
				FunctionInfo.CreateAggregate(134, FunctionName.Count, false, DataType.Integer, false, new DataType[] { DataType.DateTime }),
				FunctionInfo.CreateAggregate(209, FunctionName.Count, false, DataType.Integer, false, new DataType[] { DataType.Time }),
				FunctionInfo.CreateAggregate(135, FunctionName.Count, false, DataType.Integer, false, new DataType[] { DataType.EntityKey })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(136, FunctionName.CountDistinct, false, DataType.Integer, false, new DataType[1]),
				FunctionInfo.CreateAggregate(137, FunctionName.CountDistinct, false, DataType.Integer, false, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(138, FunctionName.CountDistinct, false, DataType.Integer, false, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(139, FunctionName.CountDistinct, false, DataType.Integer, false, new DataType[] { DataType.Float }),
				FunctionInfo.CreateAggregate(140, FunctionName.CountDistinct, false, DataType.Integer, false, new DataType[] { DataType.Boolean }),
				FunctionInfo.CreateAggregate(141, FunctionName.CountDistinct, false, DataType.Integer, false, new DataType[] { DataType.DateTime }),
				FunctionInfo.CreateAggregate(210, FunctionName.CountDistinct, false, DataType.Integer, false, new DataType[] { DataType.Time })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(142, FunctionName.StDev, false, DataType.Float, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(143, FunctionName.StDev, false, DataType.Float, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(144, FunctionName.StDev, false, DataType.Float, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(145, FunctionName.StDevP, false, DataType.Float, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(146, FunctionName.StDevP, false, DataType.Float, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(147, FunctionName.StDevP, false, DataType.Float, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(148, FunctionName.Var, false, DataType.Float, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(149, FunctionName.Var, false, DataType.Float, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(150, FunctionName.Var, false, DataType.Float, true, new DataType[] { DataType.Float })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(151, FunctionName.VarP, false, DataType.Float, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(152, FunctionName.VarP, false, DataType.Float, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(153, FunctionName.VarP, false, DataType.Float, true, new DataType[] { DataType.Float })
			});
			FunctionInfo.FunctionInfoDictionary functionInfoDictionary2 = functionInfoDictionary;
			FunctionInfo[] array = new FunctionInfo[10];
			array[0] = FunctionInfo.CreateScalar(154, FunctionName.If, DataType.Null, true, new DataType[]
			{
				DataType.Boolean,
				DataType.Null,
				DataType.Null
			});
			int num = 1;
			int num2 = 155;
			FunctionName functionName = FunctionName.If;
			DataType dataType = DataType.String;
			bool flag = true;
			DataType[] array2 = new DataType[3];
			array2[0] = DataType.Boolean;
			array[num] = FunctionInfo.CreateScalar(num2, functionName, dataType, flag, array2);
			array[2] = FunctionInfo.CreateScalar(156, FunctionName.If, DataType.Integer, true, new DataType[]
			{
				DataType.Boolean,
				DataType.Integer,
				DataType.Integer
			});
			array[3] = FunctionInfo.CreateScalar(157, FunctionName.If, DataType.Decimal, true, new DataType[]
			{
				DataType.Boolean,
				DataType.Decimal,
				DataType.Decimal
			});
			array[4] = FunctionInfo.CreateScalar(158, FunctionName.If, DataType.Float, true, new DataType[]
			{
				DataType.Boolean,
				DataType.Float,
				DataType.Float
			});
			array[5] = FunctionInfo.CreateScalar(159, FunctionName.If, DataType.Boolean, true, new DataType[]
			{
				DataType.Boolean,
				DataType.Boolean,
				DataType.Boolean
			});
			array[6] = FunctionInfo.CreateScalar(160, FunctionName.If, DataType.DateTime, true, new DataType[]
			{
				DataType.Boolean,
				DataType.DateTime,
				DataType.DateTime
			});
			array[7] = FunctionInfo.CreateScalar(211, FunctionName.If, DataType.Time, true, new DataType[]
			{
				DataType.Boolean,
				DataType.Time,
				DataType.Time
			});
			array[8] = FunctionInfo.CreateScalar(161, FunctionName.If, DataType.Binary, true, new DataType[]
			{
				DataType.Boolean,
				DataType.Binary,
				DataType.Binary
			});
			array[9] = FunctionInfo.CreateScalar(162, FunctionName.If, DataType.EntityKey, true, new DataType[]
			{
				DataType.Boolean,
				DataType.EntityKey,
				DataType.EntityKey
			});
			functionInfoDictionary2.AddGroup(array);
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateSwitch(163, FunctionName.Switch, DataType.Null),
				FunctionInfo.CreateSwitch(164, FunctionName.Switch, DataType.String),
				FunctionInfo.CreateSwitch(165, FunctionName.Switch, DataType.Integer),
				FunctionInfo.CreateSwitch(166, FunctionName.Switch, DataType.Decimal),
				FunctionInfo.CreateSwitch(167, FunctionName.Switch, DataType.Float),
				FunctionInfo.CreateSwitch(168, FunctionName.Switch, DataType.Boolean),
				FunctionInfo.CreateSwitch(169, FunctionName.Switch, DataType.DateTime),
				FunctionInfo.CreateSwitch(212, FunctionName.Switch, DataType.Time),
				FunctionInfo.CreateSwitch(170, FunctionName.Switch, DataType.Binary)
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateFilter(171, FunctionName.Filter, DataType.Null),
				FunctionInfo.CreateFilter(172, FunctionName.Filter, DataType.String),
				FunctionInfo.CreateFilter(173, FunctionName.Filter, DataType.Integer),
				FunctionInfo.CreateFilter(174, FunctionName.Filter, DataType.Decimal),
				FunctionInfo.CreateFilter(175, FunctionName.Filter, DataType.Float),
				FunctionInfo.CreateFilter(176, FunctionName.Filter, DataType.Boolean),
				FunctionInfo.CreateFilter(177, FunctionName.Filter, DataType.DateTime),
				FunctionInfo.CreateFilter(213, FunctionName.Filter, DataType.Time),
				FunctionInfo.CreateFilter(178, FunctionName.Filter, DataType.Binary),
				FunctionInfo.CreateFilter(179, FunctionName.Filter, DataType.EntityKey)
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateEvaluate(180, FunctionName.Evaluate, DataType.Null),
				FunctionInfo.CreateEvaluate(181, FunctionName.Evaluate, DataType.String),
				FunctionInfo.CreateEvaluate(182, FunctionName.Evaluate, DataType.Integer),
				FunctionInfo.CreateEvaluate(183, FunctionName.Evaluate, DataType.Decimal),
				FunctionInfo.CreateEvaluate(184, FunctionName.Evaluate, DataType.Float),
				FunctionInfo.CreateEvaluate(185, FunctionName.Evaluate, DataType.Boolean),
				FunctionInfo.CreateEvaluate(186, FunctionName.Evaluate, DataType.DateTime),
				FunctionInfo.CreateEvaluate(214, FunctionName.Evaluate, DataType.Time),
				FunctionInfo.CreateEvaluate(187, FunctionName.Evaluate, DataType.Binary),
				FunctionInfo.CreateEvaluate(188, FunctionName.Evaluate, DataType.EntityKey)
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[]
			{
				FunctionInfo.CreateAggregate(189, FunctionName.Aggregate, false, DataType.Null, true, new DataType[] { DataType.Null }),
				FunctionInfo.CreateAggregate(190, FunctionName.Aggregate, false, DataType.String, true, new DataType[1]),
				FunctionInfo.CreateAggregate(191, FunctionName.Aggregate, false, DataType.Integer, true, new DataType[] { DataType.Integer }),
				FunctionInfo.CreateAggregate(192, FunctionName.Aggregate, false, DataType.Decimal, true, new DataType[] { DataType.Decimal }),
				FunctionInfo.CreateAggregate(193, FunctionName.Aggregate, false, DataType.Float, true, new DataType[] { DataType.Float }),
				FunctionInfo.CreateAggregate(194, FunctionName.Aggregate, false, DataType.Boolean, true, new DataType[] { DataType.Boolean }),
				FunctionInfo.CreateAggregate(195, FunctionName.Aggregate, false, DataType.DateTime, true, new DataType[] { DataType.DateTime }),
				FunctionInfo.CreateAggregate(215, FunctionName.Aggregate, false, DataType.Time, true, new DataType[] { DataType.Time }),
				FunctionInfo.CreateAggregate(196, FunctionName.Aggregate, false, DataType.Binary, true, new DataType[] { DataType.Binary }),
				FunctionInfo.CreateAggregate(197, FunctionName.Aggregate, false, DataType.EntityKey, true, new DataType[] { DataType.EntityKey })
			});
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalarStatic(198, FunctionName.GetUserID, DataType.String, true, Array.Empty<DataType>()) });
			functionInfoDictionary.AddGroup(new FunctionInfo[] { FunctionInfo.CreateScalarStatic(199, FunctionName.GetUserCulture, DataType.String, true, Array.Empty<DataType>()) });
			return functionInfoDictionary;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00020BE6 File Offset: 0x0001EDE6
		private static FunctionInfo CreateScalar(int id, FunctionName functionName, DataType returnDataType, bool returnNullable, params DataType[] argumentDataTypes)
		{
			return new FunctionInfo(id, functionName, false, false, new ResultType(returnDataType, Cardinality.One, returnNullable), ResultType.FromDataTypes(argumentDataTypes, Cardinality.One, true));
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00020C02 File Offset: 0x0001EE02
		private static FunctionInfo CreateScalarStatic(int id, FunctionName functionName, DataType returnDataType, bool returnNullable, params DataType[] argumentDataTypes)
		{
			return new FunctionInfo(id, functionName, false, true, new ResultType(returnDataType, Cardinality.One, returnNullable), ResultType.FromDataTypes(argumentDataTypes, Cardinality.One, true));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00020C1E File Offset: 0x0001EE1E
		private static FunctionInfo CreateAggregate(int id, FunctionName functionName, bool transitive, DataType returnDataType, bool returnNullable, params DataType[] argumentDataTypes)
		{
			return new FunctionInfo(id, functionName, transitive, false, new ResultType(returnDataType, Cardinality.One, returnNullable), ResultType.FromDataTypes(argumentDataTypes, Cardinality.Many, true));
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00020C3C File Offset: 0x0001EE3C
		private static FunctionInfo CreateSwitch(int id, FunctionName functionName, DataType valueDataType)
		{
			if (functionName != FunctionName.Switch)
			{
				throw new InternalModelingException("Unexpected FunctionName passed to CreateSwitch");
			}
			return new FunctionInfo(id, functionName, false, false, new ResultType(valueDataType, Cardinality.One, true), new ResultType[]
			{
				new ResultType(DataType.Boolean, Cardinality.One, true),
				new ResultType(valueDataType, Cardinality.One, true)
			}, true);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00020C90 File Offset: 0x0001EE90
		private static FunctionInfo CreateIn(int id, FunctionName functionName, DataType itemDataType)
		{
			if (functionName != FunctionName.In)
			{
				throw new InternalModelingException("Unexpected FunctionName passed to CreateIn");
			}
			return new FunctionInfo(id, functionName, false, false, new ResultType(DataType.Boolean, Cardinality.One, false), new ResultType[]
			{
				new ResultType(itemDataType, Cardinality.One, true),
				new ResultType(itemDataType, Cardinality.Many, true)
			});
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00020CE4 File Offset: 0x0001EEE4
		private static FunctionInfo CreateFilter(int id, FunctionName functionName, DataType itemsDataType)
		{
			if (functionName != FunctionName.Filter)
			{
				throw new InternalModelingException("Unexpected FunctionName passed to CreateFilter");
			}
			return new FunctionInfo(id, functionName, false, false, new ResultType(itemsDataType, Cardinality.Many, true), new ResultType[]
			{
				new ResultType(itemsDataType, Cardinality.Many, true),
				new ResultType(DataType.Boolean, Cardinality.One, true)
			}, 0);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00020D38 File Offset: 0x0001EF38
		private static FunctionInfo CreateEvaluate(int id, FunctionName functionName, DataType dataType)
		{
			if (functionName != FunctionName.Evaluate)
			{
				throw new InternalModelingException("Unexpected FunctionName passed to CreateEvaluate");
			}
			return new FunctionInfo(id, functionName, false, false, new ResultType(dataType, Cardinality.Many, true), new ResultType[]
			{
				new ResultType(dataType, Cardinality.Many, true)
			}, 0);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00020D7C File Offset: 0x0001EF7C
		private FunctionInfo(int id, FunctionName functionName, bool transitive, bool _static, ResultType returnType, ResultType[] arguments)
		{
			this.m_id = id;
			this.m_functionName = functionName;
			this.m_transitive = transitive;
			this.m_static = _static;
			this.m_returnType = returnType;
			this.m_arguments = new ReadOnlyCollection<ResultType>(arguments);
			if (this.m_returnType.Cardinality == Cardinality.One)
			{
				this.m_scalar = true;
				for (int i = 0; i < this.m_arguments.Count; i++)
				{
					if (this.m_arguments[i].Cardinality != Cardinality.One)
					{
						this.m_scalar = false;
						break;
					}
				}
			}
			for (int j = 0; j < this.m_arguments.Count; j++)
			{
				if (FunctionInfo.IsAggregateArgumentCore(j, this.m_arguments, this.m_returnType))
				{
					this.m_aggregate = true;
					break;
				}
			}
			if (this.m_scalar && this.m_aggregate)
			{
				throw new InternalModelingException("Function cannot be both scalar and aggregate");
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00020E58 File Offset: 0x0001F058
		private FunctionInfo(int id, FunctionName functionName, bool transitive, bool _static, ResultType returnType, ResultType[] arguments, bool repeatingArguments)
			: this(id, functionName, transitive, _static, returnType, arguments)
		{
			this.m_repeatingArguments = repeatingArguments;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00020E74 File Offset: 0x0001F074
		private FunctionInfo(int id, FunctionName functionName, bool transitive, bool _static, ResultType returnType, ResultType[] arguments, int passthroughArgIndex)
			: this(id, functionName, transitive, _static, returnType, arguments)
		{
			if (passthroughArgIndex < 0 || passthroughArgIndex >= arguments.Length)
			{
				throw new InternalModelingException("passthroughArgIndex is out of range");
			}
			if (returnType.Cardinality != Cardinality.Many || arguments[passthroughArgIndex].Cardinality != Cardinality.Many || returnType.DataType != arguments[passthroughArgIndex].DataType)
			{
				throw new InternalModelingException("Invalid passthrough function");
			}
			this.m_passthroughArgIndex = new int?(passthroughArgIndex);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00020EF2 File Offset: 0x0001F0F2
		public FunctionName FunctionName
		{
			get
			{
				return this.m_functionName;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00020EFA File Offset: 0x0001F0FA
		public bool IsScalar
		{
			get
			{
				return this.m_scalar;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00020F02 File Offset: 0x0001F102
		public bool IsAggregate
		{
			get
			{
				return this.m_aggregate;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00020F0A File Offset: 0x0001F10A
		public bool IsTransitive
		{
			get
			{
				return this.m_transitive;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00020F12 File Offset: 0x0001F112
		public bool IsStatic
		{
			get
			{
				return this.m_static;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00020F1A File Offset: 0x0001F11A
		public ResultType ReturnType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00020F22 File Offset: 0x0001F122
		public ReadOnlyCollection<ResultType> Arguments
		{
			get
			{
				return this.m_arguments;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00020F2A File Offset: 0x0001F12A
		public bool RepeatingArguments
		{
			get
			{
				return this.m_repeatingArguments;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00020F32 File Offset: 0x0001F132
		public bool IsPassthrough
		{
			get
			{
				return this.m_passthroughArgIndex != null;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00020F3F File Offset: 0x0001F13F
		public int? PassthroughArgument
		{
			get
			{
				return this.m_passthroughArgIndex;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00020F48 File Offset: 0x0001F148
		public bool IsSetArgument(int argumentIndex)
		{
			if (this.m_repeatingArguments)
			{
				argumentIndex %= this.m_arguments.Count;
			}
			if (argumentIndex < 0 || argumentIndex >= this.m_arguments.Count)
			{
				throw new ArgumentOutOfRangeException("argumentIndex");
			}
			return this.m_arguments[argumentIndex].Cardinality == Cardinality.Many;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00020FA0 File Offset: 0x0001F1A0
		public bool IsAggregateArgument(int argumentIndex)
		{
			if (this.m_repeatingArguments)
			{
				return FunctionInfo.IsAggregateArgumentCore(argumentIndex % this.m_arguments.Count, this.m_arguments, this.m_returnType);
			}
			return FunctionInfo.IsAggregateArgumentCore(argumentIndex, this.m_arguments, this.m_returnType);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00020FDC File Offset: 0x0001F1DC
		private static bool IsAggregateArgumentCore(int argumentIndex, ReadOnlyCollection<ResultType> arguments, ResultType returnType)
		{
			if (argumentIndex < 0 || argumentIndex >= arguments.Count)
			{
				throw new ArgumentOutOfRangeException("argumentIndex");
			}
			return returnType.Cardinality == Cardinality.One && arguments[argumentIndex].Cardinality == Cardinality.Many;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002101E File Offset: 0x0001F21E
		public static ReadOnlyCollection<FunctionInfo> GetInfos(FunctionName functionName)
		{
			if (!EnumUtil.IsDefined<FunctionName>(functionName))
			{
				throw new InvalidEnumArgumentException();
			}
			return new ReadOnlyCollection<FunctionInfo>(FunctionInfo.m_functionInfos.GetGroup(functionName));
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0002103E File Offset: 0x0001F23E
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00021046 File Offset: 0x0001F246
		internal static FunctionInfo GetInfo(int id)
		{
			return FunctionInfo.m_functionInfos.GetInfo(id);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00021053 File Offset: 0x0001F253
		internal static bool IsScalarFunction(FunctionName functionName)
		{
			return FunctionInfo.m_functionInfos.GetGroup(functionName)[0].IsScalar;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00021067 File Offset: 0x0001F267
		internal static int? GetPassthroughArgument(FunctionName functionName)
		{
			return FunctionInfo.m_functionInfos.GetGroup(functionName)[0].PassthroughArgument;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002107B File Offset: 0x0001F27B
		public static FunctionInfo Resolve(FunctionName functionName, params ResultType[] arguments)
		{
			if (!EnumUtil.IsDefined<FunctionName>(functionName))
			{
				throw new InvalidEnumArgumentException();
			}
			return FunctionInfo.Resolve(functionName, arguments, null, null);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00021094 File Offset: 0x0001F294
		internal static FunctionInfo Resolve(FunctionName functionName, ResultType[] arguments, IList<Expression> argumentExprs, ValidationContext ctx)
		{
			FunctionInfo[] group = FunctionInfo.m_functionInfos.GetGroup(functionName);
			FunctionInfo functionInfo = group[0];
			int num = 0;
			for (int i = 0; i < group.Length; i++)
			{
				int num2;
				bool flag;
				if (FunctionInfo.CheckArguments(group[i], arguments, argumentExprs, null, out num2, out flag))
				{
					if (flag)
					{
						FunctionInfo.CheckArguments(group[i], arguments, argumentExprs, ctx, out num2, out flag);
					}
					return group[i];
				}
				if (num2 > num)
				{
					functionInfo = group[i];
					num = num2;
				}
			}
			if (ctx != null)
			{
				bool flag2;
				FunctionInfo.CheckArguments(functionInfo, arguments, argumentExprs, ctx, out num, out flag2);
			}
			return null;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00021108 File Offset: 0x0001F308
		internal static FunctionInfo GetFirst(FunctionName functionName, int numArguments)
		{
			FunctionInfo[] group = FunctionInfo.m_functionInfos.GetGroup(functionName);
			for (int i = 0; i < group.Length; i++)
			{
				if (FunctionInfo.NumArgsMatch(group[i], numArguments))
				{
					return group[i];
				}
			}
			return null;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002113F File Offset: 0x0001F33F
		private static bool NumArgsMatch(FunctionInfo fInfo, int numArguments)
		{
			return (fInfo.RepeatingArguments && numArguments > fInfo.Arguments.Count && numArguments % fInfo.Arguments.Count == 0) || numArguments == fInfo.Arguments.Count;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00021178 File Offset: 0x0001F378
		private static bool CheckArguments(FunctionInfo fInfo, IList<ResultType> inputArgs, IList<Expression> argumentExprs, ValidationContext ctx, out int matchScore, out bool hasWarningsToLog)
		{
			IList<ResultType> list = fInfo.Arguments;
			if (fInfo.RepeatingArguments && FunctionInfo.NumArgsMatch(fInfo, inputArgs.Count))
			{
				ResultType[] array = new ResultType[inputArgs.Count];
				for (int i = 0; i < inputArgs.Count; i += list.Count)
				{
					list.CopyTo(array, i);
				}
				list = array;
			}
			int num = 0;
			matchScore = 0;
			hasWarningsToLog = false;
			if (list.Count != inputArgs.Count)
			{
				if (ctx != null)
				{
					ctx.AddScopedError(ModelingErrorCode.WrongNumberOfArguments, SRErrors.WrongNumberOfArguments(ctx.CurrentObjectDescriptor, fInfo.FunctionName));
				}
				return false;
			}
			for (int j = 0; j < list.Count; j++)
			{
				FunctionInfo.ArgumentMatch argumentMatch = FunctionInfo.CheckArgument(list[j], inputArgs[j]);
				if ((argumentMatch & FunctionInfo.ArgumentMatch.FullMatch) == FunctionInfo.ArgumentMatch.FullMatch)
				{
					if ((argumentMatch & FunctionInfo.ArgumentMatch.DecimalToFloatAutoCast) != FunctionInfo.ArgumentMatch.NoMatch)
					{
						if (ctx != null)
						{
							ctx.AddScopedWarning(ModelingErrorCode.ImplicitDecimalCastToFloat, SRErrors.ImplicitDecimalCastToFloat(ctx.CurrentObjectDescriptor, fInfo.FunctionName, j + 1));
						}
						else
						{
							hasWarningsToLog = true;
						}
					}
					num++;
				}
				else
				{
					if ((argumentMatch & FunctionInfo.ArgumentMatch.DataTypeMatch) == FunctionInfo.ArgumentMatch.NoMatch && ctx != null)
					{
						ctx.AddScopedError(ModelingErrorCode.ArgumentDataTypeMismatch, SRErrors.ArgumentDataTypeMismatch(ctx.CurrentObjectDescriptor, fInfo.FunctionName, j + 1, inputArgs[j].DataType));
					}
					if ((argumentMatch & FunctionInfo.ArgumentMatch.CardinalityMatch) == FunctionInfo.ArgumentMatch.NoMatch && ctx != null)
					{
						ctx.AddScopedError(ModelingErrorCode.ArgumentCardinalityMismatch, SRErrors.ArgumentCardinalityMismatch(ctx.CurrentObjectDescriptor, fInfo.FunctionName, j + 1));
					}
				}
				if ((argumentMatch & FunctionInfo.ArgumentMatch.DataTypeMatch) != FunctionInfo.ArgumentMatch.NoMatch && j < 24)
				{
					matchScore += 1 << 24 - j - 1;
				}
				if (argumentExprs != null)
				{
					bool flag = false;
					if ((fInfo.FunctionName == FunctionName.DateAdd || fInfo.FunctionName == FunctionName.DateDiff) && j == 0)
					{
						if (argumentExprs[j].NodeAsLiteral == null || !argumentExprs[j].Path.IsEmpty)
						{
							if (ctx != null)
							{
								ctx.AddScopedError(ModelingErrorCode.InvalidDateIntervalArgument, SRErrors.InvalidDateIntervalArgument(ctx.CurrentObjectDescriptor, fInfo.FunctionName, j + 1));
							}
							flag = true;
						}
						else if (argumentExprs[j].NodeAsLiteral.DataType == DataType.String && argumentExprs[j].NodeAsLiteral.Cardinality == Cardinality.One && !FunctionInfo.DateIntervals.Contains(argumentExprs[j].NodeAsLiteral.ValueAsString))
						{
							if (ctx != null)
							{
								ctx.AddScopedError(ModelingErrorCode.InvalidDateIntervalValue, SRErrors.InvalidDateIntervalValue(ctx.CurrentObjectDescriptor, fInfo.FunctionName, j + 1, argumentExprs[j].NodeAsLiteral.ValueAsString, StringUtil.Join(", ", FunctionInfo.DateIntervals)));
							}
							flag = true;
						}
					}
					if (fInfo.FunctionName == FunctionName.In && j == 1)
					{
						ResultType? resultType = argumentExprs[j].TryGetResultType();
						if (!argumentExprs[j].Node.IsConstantValue || (resultType != null && resultType.Value.Nullable) || !argumentExprs[j].Path.IsEmpty)
						{
							if (ctx != null)
							{
								ctx.AddScopedError(ModelingErrorCode.InvalidInSetArgument, SRErrors.InvalidInSetArgument(ctx.CurrentObjectDescriptor, fInfo.FunctionName, j + 1));
							}
							flag = true;
						}
					}
					else if (argumentExprs[j].NodeAsLiteral != null && argumentExprs[j].NodeAsLiteral.Cardinality == Cardinality.Many)
					{
						if (ctx != null)
						{
							ctx.AddScopedError(ModelingErrorCode.InvalidLiteralSetArgument, SRErrors.InvalidLiteralSetArgument(ctx.CurrentObjectDescriptor, fInfo.FunctionName, j + 1));
						}
						flag = true;
					}
					if (flag)
					{
						num--;
					}
				}
			}
			matchScore += num << 24;
			return num == list.Count;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000214E0 File Offset: 0x0001F6E0
		private static FunctionInfo.ArgumentMatch CheckArgument(ResultType funcArg, ResultType inputArg)
		{
			if (!funcArg.Nullable)
			{
				throw new InternalModelingException("Function arguments should be nullable");
			}
			FunctionInfo.ArgumentMatch argumentMatch = FunctionInfo.ArgumentMatch.NoMatch;
			if (funcArg.DataType == inputArg.DataType || inputArg.DataType == DataType.Null)
			{
				argumentMatch |= FunctionInfo.ArgumentMatch.DataTypeMatch;
			}
			else if (inputArg.DataType == DataType.Integer && (funcArg.DataType == DataType.Decimal || funcArg.DataType == DataType.Float))
			{
				argumentMatch |= FunctionInfo.ArgumentMatch.DataTypeMatch;
			}
			else if (inputArg.DataType == DataType.Decimal && funcArg.DataType == DataType.Float)
			{
				argumentMatch |= FunctionInfo.ArgumentMatch.DataTypeMatch | FunctionInfo.ArgumentMatch.DecimalToFloatAutoCast;
			}
			if (funcArg.Cardinality == inputArg.Cardinality || (funcArg.Cardinality == Cardinality.Many && inputArg.Cardinality == Cardinality.One))
			{
				argumentMatch |= FunctionInfo.ArgumentMatch.CardinalityMatch;
			}
			return argumentMatch;
		}

		// Token: 0x040003F2 RID: 1010
		public const int FilterItemsArgument = 0;

		// Token: 0x040003F3 RID: 1011
		public const int FilterConditionArgument = 1;

		// Token: 0x040003F4 RID: 1012
		public const int IfConditionArgument = 0;

		// Token: 0x040003F5 RID: 1013
		public const int SwitchConditionArgumentMod = 2;

		// Token: 0x040003F6 RID: 1014
		public const int InItemArgument = 0;

		// Token: 0x040003F7 RID: 1015
		public const int InSetArgument = 1;

		// Token: 0x040003F8 RID: 1016
		private const int DateAddDiffIntervalArgument = 0;

		// Token: 0x040003F9 RID: 1017
		private static readonly ReadOnlyCollection<string> DateIntervals = new ReadOnlyCollection<string>(new string[] { "Year", "Quarter", "Month", "Day", "Hour", "Minute", "Second", "Week" });

		// Token: 0x040003FA RID: 1018
		private static readonly FunctionInfo.FunctionInfoDictionary m_functionInfos = FunctionInfo.CreateFunctionInfos();

		// Token: 0x040003FB RID: 1019
		private readonly int m_id;

		// Token: 0x040003FC RID: 1020
		private readonly FunctionName m_functionName;

		// Token: 0x040003FD RID: 1021
		private readonly bool m_scalar;

		// Token: 0x040003FE RID: 1022
		private readonly bool m_aggregate;

		// Token: 0x040003FF RID: 1023
		private readonly bool m_transitive;

		// Token: 0x04000400 RID: 1024
		private readonly bool m_static;

		// Token: 0x04000401 RID: 1025
		private readonly ResultType m_returnType;

		// Token: 0x04000402 RID: 1026
		private readonly ReadOnlyCollection<ResultType> m_arguments;

		// Token: 0x04000403 RID: 1027
		private readonly bool m_repeatingArguments;

		// Token: 0x04000404 RID: 1028
		private readonly int? m_passthroughArgIndex;

		// Token: 0x020001AA RID: 426
		private class FunctionInfoDictionary
		{
			// Token: 0x060010C1 RID: 4289 RVA: 0x000348FD File Offset: 0x00032AFD
			internal FunctionInfoDictionary()
			{
			}

			// Token: 0x060010C2 RID: 4290 RVA: 0x0003491C File Offset: 0x00032B1C
			internal void AddGroup(params FunctionInfo[] functionInfos)
			{
				IList<ResultType> list = functionInfos[0].Arguments;
				for (int i = 1; i < functionInfos.Length; i++)
				{
					if (functionInfos[i].FunctionName != functionInfos[0].FunctionName)
					{
						throw new InternalModelingException("FunctionName mismatch in FunctionInfo group");
					}
					if (functionInfos[i].IsScalar != functionInfos[0].IsScalar)
					{
						throw new InternalModelingException("IsScalar mismatch in FunctionInfo group");
					}
					if (functionInfos[i].IsAggregate != functionInfos[0].IsAggregate)
					{
						throw new InternalModelingException("IsAggregate mismatch in FunctionInfo group");
					}
					if (functionInfos[i].IsTransitive != functionInfos[0].IsTransitive)
					{
						throw new InternalModelingException("IsTransitive mismatch in FunctionInfo group");
					}
					if (functionInfos[i].ReturnType.Cardinality != functionInfos[0].ReturnType.Cardinality)
					{
						throw new InternalModelingException("ReturnType.Cardinality mismatch in FunctionInfo group");
					}
					if (functionInfos[i].RepeatingArguments != functionInfos[0].RepeatingArguments)
					{
						throw new InternalModelingException("RepeatingArguments mismatch in FunctionInfo group");
					}
					int? passthroughArgument = functionInfos[i].PassthroughArgument;
					int? passthroughArgument2 = functionInfos[0].PassthroughArgument;
					if (!((passthroughArgument.GetValueOrDefault() == passthroughArgument2.GetValueOrDefault()) & (passthroughArgument != null == (passthroughArgument2 != null))))
					{
						throw new InternalModelingException("PassthroughArgument mismatch in FunctionInfo group");
					}
					IList<ResultType> arguments = functionInfos[i].Arguments;
					if (arguments.Count == list.Count)
					{
						for (int j = 0; j < arguments.Count; j++)
						{
							if (arguments[j].Cardinality != list[j].Cardinality)
							{
								throw new InternalModelingException("Arguments[" + j.ToString() + "].Cardinality mismatch in FunctionInfo group");
							}
						}
					}
					else
					{
						if (arguments.Count >= list.Count)
						{
							throw new InternalModelingException("FunctionInfos out of order (expected descending by number of arguments)");
						}
						list = arguments;
					}
				}
				this.m_dict.Add(functionInfos[0].FunctionName, functionInfos);
				for (int k = 0; k < functionInfos.Length; k++)
				{
					this.m_ids.Add(functionInfos[k].ID, functionInfos[k]);
				}
			}

			// Token: 0x060010C3 RID: 4291 RVA: 0x00034B10 File Offset: 0x00032D10
			internal FunctionInfo[] GetGroup(FunctionName functionName)
			{
				FunctionInfo[] array;
				if (this.m_dict.TryGetValue(functionName, out array))
				{
					return array;
				}
				throw new InternalModelingException("Missing function signatures for " + functionName.ToString());
			}

			// Token: 0x060010C4 RID: 4292 RVA: 0x00034B4B File Offset: 0x00032D4B
			internal FunctionInfo GetInfo(int id)
			{
				return this.m_ids[id];
			}

			// Token: 0x040006FC RID: 1788
			private readonly Dictionary<FunctionName, FunctionInfo[]> m_dict = new Dictionary<FunctionName, FunctionInfo[]>();

			// Token: 0x040006FD RID: 1789
			private readonly Dictionary<int, FunctionInfo> m_ids = new Dictionary<int, FunctionInfo>();
		}

		// Token: 0x020001AB RID: 427
		[Flags]
		private enum ArgumentMatch
		{
			// Token: 0x040006FF RID: 1791
			NoMatch = 0,
			// Token: 0x04000700 RID: 1792
			DataTypeMatch = 1,
			// Token: 0x04000701 RID: 1793
			CardinalityMatch = 2,
			// Token: 0x04000702 RID: 1794
			DecimalToFloatAutoCast = 4,
			// Token: 0x04000703 RID: 1795
			FullMatch = 3
		}
	}
}
