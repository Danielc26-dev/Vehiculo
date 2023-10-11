using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehiculo : MonoBehaviour
{
    public float radio;
    public float maxSpeed;
    public float Speed;
    public float RotationSpeed;
    public float radioWander;
    public Manager_IA _ManagerIA;
    public float rateWander;
    private float frameRateWander = 0;

    public void Seek(Vector3 Target)
    {
        Vector3 direccion = (Target - transform.position).normalized;
        transform.position += direccion * Time.deltaTime * maxSpeed;
    }

    public void Arrive(Vector3 Target)
    {
        Vector3 direccion = (Target - transform.position);

        float d = direccion.magnitude;

        direccion.Normalize();

        if (d < radio)
        {
            Speed = Mathf.Clamp(d / radio, 0, radio);
        }
        else
        {
            Speed = Mathf.Lerp(Speed, maxSpeed, Time.deltaTime * 5);
        }

        transform.position += direccion * Time.deltaTime * Speed;
    }
    public void LookTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * RotationSpeed);
    }
    public void wander()
    {
        Vector3 rnd = Vector3.zero;
        Vector3 Target = Vector3.zero;


        if (frameRateWander > rateWander)
        {
            rnd = Random.insideUnitSphere * radioWander;
            rnd.y = 0;
            Target = transform.position + transform.forward + rnd;
            frameRateWander = 0;
            transform.rotation = Quaternion.LookRotation((Target - transform.position).normalized);
        }
        frameRateWander += Time.deltaTime;

        transform.position += transform.forward * Time.deltaTime * Speed;
    }

    public void Separation(float distanceSep)
    {
        Vector3 FuerzaDeSeparacion = Vector3.zero;

        foreach (var item in _ManagerIA.vehicles)
        {
            if (item != this)
            {
                Vector3 HaciaOtro = transform.position - item.transform.position;
                float DistanciaEntreOtros = HaciaOtro.magnitude;

                if (DistanciaEntreOtros < distanceSep)
                {
                    float strength = Mathf.Clamp01((distanceSep - DistanciaEntreOtros) / distanceSep);
                    FuerzaDeSeparacion += HaciaOtro.normalized * strength;
                }
            }
        }

        FuerzaDeSeparacion.y = 0;
        transform.position += FuerzaDeSeparacion * Time.deltaTime * maxSpeed;
    }


    public void Cohesion(float cohesionDistance)
    {
        //Vector3 centerOfMass = Vector3.zero;
        //int vehicleCount = 0;

        //foreach (var item in _ManagerIA.vehicles)
        //{
        //    if (item != this)
        //    {
        //        Vector3 haciaOtro = item.transform.position - transform.position;
        //        float distanciaEntreOtros = haciaOtro.magnitude;

        //        if (distanciaEntreOtros < cohesionDistance)
        //        {
        //            centerOfMass += item.transform.position;
        //            vehicleCount++;
        //        }
        //    }
        //}

        //if (vehicleCount > 0)
        //{
        //    centerOfMass /= vehicleCount;
        //    Vector3 haciaCentro = centerOfMass - transform.position;
        //    haciaCentro.Normalize();
        //    transform.position += haciaCentro * Time.deltaTime * maxSpeed;

        //    // Aplicar rotación para mirar hacia el centro de masas
        //    Quaternion targetRotation = Quaternion.LookRotation(haciaCentro);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
        //}
    }

    //public void followMyPath()
    //{
    //    //Seek(myPath.nextPoint(transform.position));
    //}
}