﻿namespace Business.Utilities.Results
{
    public interface IResult
    {
        bool Succes { get; }
        string Message { get; }
    }
}
