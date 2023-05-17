using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/imc")]
public class IMCCalculatorController : ControllerBase
{
    [HttpPost("calcular")]
    public IActionResult CalcularIMC([FromBody] IMCModel model)
    {
        if (model.Peso <= 0 || model.Altura <= 0)
        {
            return BadRequest("O peso e a altura devem ser maiores do que 0");
        }

        double imc = model.Peso / (model.Altura * model.Altura);
        double imcArredondado = Math.Round(imc, 1);

        string faixaPeso = GetFaixaPeso(imc);

        var resultado = new
        {
            IMC = imcArredondado,
            FaixaPeso = faixaPeso
        };

        return Ok(resultado);
    }
    private string GetFaixaPeso(double imc)
    {
        string faixaPeso; 

        if (imc < 18.5)
        {
            faixaPeso = "Abaixo do peso";
        }

        else if (imc < 25)
        {
            faixaPeso = "Peso normal";
        }
        else if (imc < 30)
        {
            faixaPeso = "Sobrepeso";
        }
        else
        {
            faixaPeso = "Obesidade";
        }
        return faixaPeso;
    }

}
