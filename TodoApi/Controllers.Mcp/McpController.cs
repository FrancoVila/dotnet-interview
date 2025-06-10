using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class McpTool
{
    public string name { get; set; }
    public string description { get; set; }
    public object input_schema { get; set; }
    public object endpoint { get; set; }
}

[ApiController]
[Route(".well-known/mcp")]
public class McpController : ControllerBase
{
    [HttpGet("tools")]
    public IActionResult GetTools()
    {
        string baseUrl = $"{Request.Scheme}://{Request.Host}";

        var tools = new List<McpTool>
        {
            new McpTool {
                name = "create_todo_item",
                description = "Crear un ítem en una lista con descripción.",
                input_schema = new {
                    type = "object",
                    properties = new {
                        todoListId = new { type = "integer" },
                        description = new { type = "string" }
                    },
                    required = new[] { "todoListId", "description" }
                },
                endpoint = new { url = $"{baseUrl}/api/todoitems", method = "POST" }
            },
            new McpTool {
                name = "update_todo_item",
                description = "Actualizar la descripción de un ítem.",
                input_schema = new {
                    type = "object",
                    properties = new {
                        id = new { type = "integer" },
                        description = new { type = "string" }
                    },
                    required = new[] { "id", "description" }
                },
                endpoint = new { url = $"{baseUrl}/api/todoitems/{{id}}", method = "PUT" }
            },
            new McpTool {
                name = "complete_todo_item",
                description = "Marcar un ítem como completado.",
                input_schema = new {
                    type = "object",
                    properties = new {
                        id = new { type = "integer" }
                    },
                    required = new[] { "id" }
                },
                endpoint = new { url = $"{baseUrl}/api/todoitems/{{id}}/complete", method = "POST" }
            },
            new McpTool {
                name = "delete_todo_item",
                description = "Eliminar un ítem.",
                input_schema = new {
                    type = "object",
                    properties = new {
                        id = new { type = "integer" }
                    },
                    required = new[] { "id" }
                },
                endpoint = new { url = $"{baseUrl}/api/todoitems/{{id}}", method = "DELETE" }
            },
            new McpTool {
                name = "list_todo_items",
                description = "Listar todos los ítems de una lista.",
                input_schema = new {
                    type = "object",
                    properties = new {
                        todoListId = new { type = "integer" }
                    },
                    required = new[] { "todoListId" }
                },
                endpoint = new { url = $"{baseUrl}/api/todolists/{{todoListId}}/items", method = "GET" }
            }
        };

        return Ok(tools);
    }
}