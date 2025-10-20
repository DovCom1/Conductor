namespace Conductor.Models;

public record ErrorResponse(
    string Error,
    int StatusCode);