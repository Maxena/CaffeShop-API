using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caffe.Application.Common.Interfaces.Presistence;

namespace Caffe.Infrastructure.Services.Base;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}