### **# Métricas de Rendimiento**



Este documento presenta los resultados obtenidos durante las pruebas de

rendimiento del sistema FraudShield. Se comparó la ejecución secuencial

con la ejecución paralela utilizando diferentes cantidades de núcleos.

###### 

###### **## Configuración de las pruebas**



\- Transacciones analizadas: 1,000,000

\- Transacciones fraudulentas generadas: 50,000

\- Procesadores disponibles: 8

\- Versión secuencial utilizada como referencia: 51,816 ms





###### 

###### **## 1. Comparación según cantidad de núcleos**



|**Núcleos**|**Tiempo paralelo (ms)**|**Speedup**|**Eficiencia**|
|-|-|-|-|
|1|53,327|0.97x|97.17%|
|2|29,719|1.74x|87.18%|
|4|17,272|3.00x|75.00% |
|6|14,093|3.68x|61.28%|
|8|13,098|3.96x|49.45%|









###### **## 2. Comparación secuencial y paralela**



La ejecución secuencial de un millón de transacciones tuvo un tiempo

de 51,816 ms.



La mejor ejecución paralela se obtuvo utilizando 8 núcleos, con un

tiempo de 13,098 ms.



|**Versión**|**Núcleos**|**Tiempo (ms)**|
|-|-|-|
|Secuencial|-|51,816|
|Paralela|1|53,327|
|Paralela|2|29,719|
|Paralela|4|17,272|
|Paralela|6|14,093|
|Paralela|8|13,098|









###### **## 3. Speedup**



El Speedup permite medir cuánto más rápida es la ejecución paralela

respecto a la ejecución secuencial.



La fórmula utilizada es:



Speedup = Tiempo secuencial / Tiempo paralelo



El mayor Speedup obtenido fue de 3.96x utilizando 8 núcleos.



Esto significa que, para esta prueba, la versión paralela con 8 núcleos

fue aproximadamente cuatro veces más rápida que la versión secuencial.









###### **## 4. Eficiencia**



La eficiencia permite observar qué tan bien se aprovechan los núcleos

utilizados durante la ejecución paralela.



La fórmula utilizada es:



Eficiencia = Speedup / Núcleos × 100



Los resultados obtenidos fueron:



|**Núcleos**|**Speedup**|**Eficiencia**|
|-|-|-|
|1|0.97x|97.17%|
|2| 1.74x|87.18%|
|4|3.00x|75.00%|
|6|3.68x|61.28%|
|8|3.96x|49.45%|









###### **## 5. Análisis de escalabilidad**



Los resultados muestran que aumentar la cantidad de núcleos reduce

considerablemente el tiempo de ejecución.



Al utilizar un núcleo, la ejecución paralela tardó 53,327 ms, incluso

ligeramente más que la versión secuencial. Esto se debe al costo

adicional asociado al procesamiento paralelo.



Con 2 núcleos, el tiempo disminuyó a 29,719 ms. Al utilizar 4 núcleos

se redujo a 17,272 ms y con 6 núcleos llegó a 14,093 ms.



La mejor medición se obtuvo con 8 núcleos, alcanzando un tiempo de

13,098 ms y un Speedup de 3.96x.



Sin embargo, el aumento de núcleos no produce una mejora proporcional

en el rendimiento. Aunque pasar de 1 a 8 núcleos aumenta el nivel de

paralelismo, la eficiencia disminuye desde 97.17% hasta 49.45%.



Esto demuestra que existen costos adicionales asociados al paralelismo

y que no todo el procesamiento puede ejecutarse de manera

completamente paralela.









###### **## 6. Cuellos de botella y limitaciones**



Durante las pruebas se observó que aumentar la cantidad de núcleos

mejora el tiempo de ejecución, pero la mejora comienza a ser menor a

medida que se utilizan más núcleos.



Una de las principales limitaciones identificadas corresponde al

analizador de frecuencia, debido a que necesita consultar el historial

de transacciones para realizar su análisis.



Además, el procesamiento paralelo presenta costos asociados a la

gestión de los hilos y al acceso concurrente a los resultados.



Por esta razón, utilizar todos los núcleos disponibles no significa

obtener una mejora proporcional del rendimiento.









###### **## 7. Conclusión de las pruebas**



Las pruebas realizadas demuestran que la implementación paralela

mejora significativamente el rendimiento del sistema al procesar

grandes cantidades de transacciones.



Para un conjunto de 1,000,000 de transacciones, la versión secuencial

tardó 51,816 ms, mientras que la versión paralela con 8 núcleos tardó

13,098 ms.



El mejor resultado obtenido fue un Speedup de 3.96x, demostrando que

el procesamiento paralelo permitió reducir considerablemente el tiempo

de ejecución.



No obstante, la eficiencia disminuyó al aumentar la cantidad de

núcleos, lo que evidencia las limitaciones propias del procesamiento

paralelo y la existencia de partes del sistema que no pueden

paralelizarse completamente.

