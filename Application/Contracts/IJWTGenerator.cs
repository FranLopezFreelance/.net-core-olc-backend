using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts
{
    public interface IJWTGenerator
    {
        string TokenGenerate(User user);
    }
}
