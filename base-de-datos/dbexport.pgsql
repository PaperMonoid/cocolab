--
-- PostgreSQL database dump
--

-- Dumped from database version 10.6
-- Dumped by pg_dump version 10.6

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- Name: actualizar_ubicacion(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.actualizar_ubicacion() RETURNS trigger
    LANGUAGE plpgsql
    AS $$declare begin
insert into Public."Historial_Maquina" ("Fecha_Registro_hist_maq","No_maquina","Id_ubicacion") 
values (LOCALTIMESTAMP,NEW."Id_ubicacion",NEW."No_Maquina_ubicacion");
return null;
end;
$$;


ALTER FUNCTION public.actualizar_ubicacion() OWNER TO postgres;

--
-- Name: actualizar_ubicacion_mod(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.actualizar_ubicacion_mod() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
declare begin
if (NEW."Estatus_ubicacion" = False) then
update public."Historial_Maquina" set "Fecha_Final_hist_maq" = localtimestamp where "Id_ubicacion" = New."Id_ubicacion";
else
end if;
return null;
end
$$;


ALTER FUNCTION public.actualizar_ubicacion_mod() OWNER TO postgres;

--
-- Name: reportecarrera(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.reportecarrera(carrera character varying) RETURNS TABLE(no_control integer, fecha_solicitud timestamp without time zone, fecha_finalizacion timestamp without time zone, uso character varying, ubicacion integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
 RETURN QUERY SELECT
 cast( "Registro"."No_Control_alumno" as integer),
 cast( "Fecha_Solicitud_registro" as timestamp),
 cast("Fecha_Finalizacion_registro" as timestamp),
 cast( "Uso_registro" as varchar),
 cast("Id_ubicacion" as integer)
 FROM
 public."Registro"
 inner join Public."Alumno" on public."Registro"."No_Control_alumno" = public."Alumno"."No_Control_alumno"
 WHERE
 public."Alumno"."Id_carrera" = carrera ;
END; $$;


ALTER FUNCTION public.reportecarrera(carrera character varying) OWNER TO postgres;

--
-- Name: reportedia(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.reportedia() RETURNS TABLE(no_control integer, fecha_solicitud timestamp without time zone, fecha_finalizacion timestamp without time zone, uso character varying, ubicacion integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
 RETURN QUERY SELECT
 cast( "Registro"."No_Control_alumno" as integer),
 cast( "Fecha_Solicitud_registro" as timestamp),
 cast("Fecha_Finalizacion_registro" as timestamp),
 cast( "Uso_registro" as varchar),
 cast("Id_ubicacion" as integer)
 FROM
 public."Registro"
 WHERE
 extract(day from "Fecha_Solicitud_registro") = extract(day from current_timestamp) ;
END; $$;


ALTER FUNCTION public.reportedia() OWNER TO postgres;

--
-- Name: reportehora(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.reportehora(hora integer) RETURNS TABLE(no_control integer, fecha_solicitud timestamp without time zone, fecha_finalizacion timestamp without time zone, uso character varying, ubicacion integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
 RETURN QUERY SELECT
 cast( "Registro"."No_Control_alumno" as integer),
 cast( "Fecha_Solicitud_registro" as timestamp),
 cast("Fecha_Finalizacion_registro" as timestamp),
 cast( "Uso_registro" as varchar),
 cast("Id_ubicacion" as integer)
 FROM
 public."Registro"
 WHERE
 extract(hour from "Fecha_Solicitud_registro") = hora ;
END; $$;


ALTER FUNCTION public.reportehora(hora integer) OWNER TO postgres;

--
-- Name: reportemes(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.reportemes(mes integer) RETURNS TABLE(no_control integer, fecha_solicitud timestamp without time zone, fecha_finalizacion timestamp without time zone, uso character varying, ubicacion integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
 RETURN QUERY SELECT
 cast( "Registro"."No_Control_alumno" as integer),
 cast( "Fecha_Solicitud_registro" as timestamp),
 cast("Fecha_Finalizacion_registro" as timestamp),
 cast( "Uso_registro" as varchar),
 cast("Id_ubicacion" as integer)
 FROM
 public."Registro"
 WHERE
 extract(month from "Fecha_Solicitud_registro") = mes ;
END; $$;


ALTER FUNCTION public.reportemes(mes integer) OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: Alumno; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Alumno" (
    "No_Control_alumno" numeric(8,0) NOT NULL,
    "Nombre_alumno" character varying(50) NOT NULL,
    "Ape_Pat_alumno" character varying(50) NOT NULL,
    "Apellido_Mat_alumno" character varying(50) NOT NULL,
    "Id_carrera" character varying(4) NOT NULL,
    "Estatus_alumno" boolean NOT NULL,
    "Fecha_Regis_alumno" timestamp without time zone NOT NULL,
    "Fecha_Mod_alumno" timestamp without time zone
);


ALTER TABLE public."Alumno" OWNER TO postgres;

--
-- Name: Carrera; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Carrera" (
    "Id_carrera" character varying(4) NOT NULL,
    "Descrip_carrera" character varying(100) NOT NULL
);


ALTER TABLE public."Carrera" OWNER TO postgres;

--
-- Name: Computadora; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Computadora" (
    "Estatus_computadora" boolean NOT NULL,
    "Serie_computadora" character varying(30) NOT NULL,
    "Marca_computadora" character varying(25) NOT NULL,
    "Modelo_computadora" character varying(30) NOT NULL,
    "No_Inventario_computadora" character varying(30) NOT NULL,
    "Fecha_Registro_computadora" timestamp without time zone NOT NULL,
    "Fecha_Mod_computadora" timestamp without time zone,
    "No_maquina" integer NOT NULL
);


ALTER TABLE public."Computadora" OWNER TO postgres;

--
-- Name: Computadora_No_maquina_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Computadora_No_maquina_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Computadora_No_maquina_seq" OWNER TO postgres;

--
-- Name: Computadora_No_maquina_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Computadora_No_maquina_seq" OWNED BY public."Computadora"."No_maquina";


--
-- Name: Historial_Maquina; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Historial_Maquina" (
    "Id_Historial_maquina" integer NOT NULL,
    "Fecha_Registro_hist_maq" timestamp without time zone NOT NULL,
    "Fecha_Final_hist_maq" timestamp without time zone,
    "No_maquina" integer NOT NULL,
    "Id_ubicacion" integer NOT NULL
);


ALTER TABLE public."Historial_Maquina" OWNER TO postgres;

--
-- Name: Historial_Maquina_Id_Historial_maquina_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Historial_Maquina_Id_Historial_maquina_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Historial_Maquina_Id_Historial_maquina_seq" OWNER TO postgres;

--
-- Name: Historial_Maquina_Id_Historial_maquina_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Historial_Maquina_Id_Historial_maquina_seq" OWNED BY public."Historial_Maquina"."Id_Historial_maquina";


--
-- Name: Registro; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Registro" (
    "Id_registro" integer NOT NULL,
    "No_Control_alumno" numeric(8,0) NOT NULL,
    "Fecha_Solicitud_registro" timestamp without time zone NOT NULL,
    "Fecha_Finalizacion_registro" timestamp without time zone,
    "Uso_registro" text NOT NULL,
    "Id_ubicacion" integer NOT NULL
);


ALTER TABLE public."Registro" OWNER TO postgres;

--
-- Name: Registro_Id_registro_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Registro_Id_registro_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Registro_Id_registro_seq" OWNER TO postgres;

--
-- Name: Registro_Id_registro_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Registro_Id_registro_seq" OWNED BY public."Registro"."Id_registro";


--
-- Name: Ubicacion; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Ubicacion" (
    "Id_ubicacion" integer NOT NULL,
    "Estatus_ubicacion" boolean NOT NULL,
    "Fecha_Registro_ubicacion" timestamp without time zone NOT NULL,
    "Fecha_Mod_ubicacion" timestamp without time zone,
    "Comentario_ubicacion" text NOT NULL,
    "No_Maquina_ubicacion" integer NOT NULL
);


ALTER TABLE public."Ubicacion" OWNER TO postgres;

--
-- Name: conteoanio; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.conteoanio AS
 SELECT date_part('year'::text, "Registro"."Fecha_Solicitud_registro") AS anio,
    count("Registro"."Id_registro") AS count
   FROM public."Registro"
  GROUP BY (date_part('year'::text, "Registro"."Fecha_Solicitud_registro"));


ALTER TABLE public.conteoanio OWNER TO postgres;

--
-- Name: conteocarrera; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.conteocarrera AS
 SELECT alum."Id_carrera",
    count("Registro"."Id_registro") AS count
   FROM (public."Registro"
     JOIN public."Alumno" alum ON (("Registro"."No_Control_alumno" = alum."No_Control_alumno")))
  GROUP BY alum."Id_carrera";


ALTER TABLE public.conteocarrera OWNER TO postgres;

--
-- Name: conteodia; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.conteodia AS
 SELECT date_part('day'::text, "Registro"."Fecha_Solicitud_registro") AS dia,
    count("Registro"."Id_registro") AS count
   FROM public."Registro"
  GROUP BY (date_part('day'::text, "Registro"."Fecha_Solicitud_registro"));


ALTER TABLE public.conteodia OWNER TO postgres;

--
-- Name: conteomes; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.conteomes AS
 SELECT date_part('month'::text, "Registro"."Fecha_Solicitud_registro") AS mes,
    count("Registro"."Id_registro") AS count
   FROM public."Registro"
  GROUP BY (date_part('month'::text, "Registro"."Fecha_Solicitud_registro"));


ALTER TABLE public.conteomes OWNER TO postgres;

--
-- Name: Computadora No_maquina; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Computadora" ALTER COLUMN "No_maquina" SET DEFAULT nextval('public."Computadora_No_maquina_seq"'::regclass);


--
-- Name: Historial_Maquina Id_Historial_maquina; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Historial_Maquina" ALTER COLUMN "Id_Historial_maquina" SET DEFAULT nextval('public."Historial_Maquina_Id_Historial_maquina_seq"'::regclass);


--
-- Name: Registro Id_registro; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Registro" ALTER COLUMN "Id_registro" SET DEFAULT nextval('public."Registro_Id_registro_seq"'::regclass);


--
-- Data for Name: Alumno; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Alumno" ("No_Control_alumno", "Nombre_alumno", "Ape_Pat_alumno", "Apellido_Mat_alumno", "Id_carrera", "Estatus_alumno", "Fecha_Regis_alumno", "Fecha_Mod_alumno") FROM stdin;
1212033	Claudio	Velocidad	Ramos	ISIC	f	2018-11-27 19:47:46.940997	2018-12-01 23:16:47.99363
\.


--
-- Data for Name: Carrera; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Carrera" ("Id_carrera", "Descrip_carrera") FROM stdin;
ISIC	Ingeniería en sistemas computacionales
IIND	Ingeniería Industrial
COPU	Contador Público
ARQU	Arquitectura
IAMB	Ingeniería Ambiental
ITIC	Ing. en Tecnologias de la Información y Comunicación
IINF	Ingeniería en Informatica
\.


--
-- Data for Name: Computadora; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Computadora" ("Estatus_computadora", "Serie_computadora", "Marca_computadora", "Modelo_computadora", "No_Inventario_computadora", "Fecha_Registro_computadora", "Fecha_Mod_computadora", "No_maquina") FROM stdin;
t	LXMX-001	Lanix	AFD-08	TA123	2018-11-27 19:59:15.338174	\N	1
f	ASDF-002	Generica	123SA	TA122	2018-11-27 19:59:15.338174	\N	2
\.


--
-- Data for Name: Historial_Maquina; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Historial_Maquina" ("Id_Historial_maquina", "Fecha_Registro_hist_maq", "Fecha_Final_hist_maq", "No_maquina", "Id_ubicacion") FROM stdin;
1	2018-11-27 20:07:01.873826	2018-12-01 22:39:37.044649	1	1
\.


--
-- Data for Name: Registro; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Registro" ("Id_registro", "No_Control_alumno", "Fecha_Solicitud_registro", "Fecha_Finalizacion_registro", "Uso_registro", "Id_ubicacion") FROM stdin;
2	1212033	2018-12-01 00:00:00	\N	tarea	1
3	1212033	2018-12-02 00:00:00	\N	tarea	1
\.


--
-- Data for Name: Ubicacion; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Ubicacion" ("Id_ubicacion", "Estatus_ubicacion", "Fecha_Registro_ubicacion", "Fecha_Mod_ubicacion", "Comentario_ubicacion", "No_Maquina_ubicacion") FROM stdin;
1	f	2018-11-27 20:07:01.873826	\N	Maquina nueva	1
\.


--
-- Name: Computadora_No_maquina_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Computadora_No_maquina_seq"', 1, false);


--
-- Name: Historial_Maquina_Id_Historial_maquina_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Historial_Maquina_Id_Historial_maquina_seq"', 2, true);


--
-- Name: Registro_Id_registro_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Registro_Id_registro_seq"', 3, true);


--
-- Name: Carrera Carrera_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Carrera"
    ADD CONSTRAINT "Carrera_pkey" PRIMARY KEY ("Id_carrera");


--
-- Name: Computadora Computadora_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Computadora"
    ADD CONSTRAINT "Computadora_pkey" PRIMARY KEY ("No_maquina");


--
-- Name: Historial_Maquina Historial_Maquina_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Historial_Maquina"
    ADD CONSTRAINT "Historial_Maquina_pkey" PRIMARY KEY ("Id_Historial_maquina");


--
-- Name: Registro Registro_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Registro"
    ADD CONSTRAINT "Registro_pkey" PRIMARY KEY ("Id_registro");


--
-- Name: Ubicacion Ubicacion_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Ubicacion"
    ADD CONSTRAINT "Ubicacion_pkey" PRIMARY KEY ("Id_ubicacion");


--
-- Name: Alumno alumno_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Alumno"
    ADD CONSTRAINT alumno_pkey PRIMARY KEY ("No_Control_alumno");


--
-- Name: fki_alumno_carrera; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_alumno_carrera ON public."Alumno" USING btree ("Id_carrera");


--
-- Name: fki_historial ubicacion; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_historial ubicacion" ON public."Historial_Maquina" USING btree ("No_maquina");


--
-- Name: fki_registro_alumno; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_registro_alumno ON public."Registro" USING btree ("No_Control_alumno");


--
-- Name: fki_registro_ubicacion; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_registro_ubicacion ON public."Registro" USING btree ("Id_ubicacion");


--
-- Name: fki_ubicacion_maquina; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_ubicacion_maquina ON public."Ubicacion" USING btree ("No_Maquina_ubicacion");


--
-- Name: Ubicacion insertar_ubicacion; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER insertar_ubicacion AFTER INSERT ON public."Ubicacion" FOR EACH ROW EXECUTE PROCEDURE public.actualizar_ubicacion();


--
-- Name: Ubicacion quitar_maquina; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER quitar_maquina AFTER UPDATE ON public."Ubicacion" FOR EACH ROW EXECUTE PROCEDURE public.actualizar_ubicacion_mod();


--
-- Name: Alumno alumno_carrera; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Alumno"
    ADD CONSTRAINT alumno_carrera FOREIGN KEY ("Id_carrera") REFERENCES public."Carrera"("Id_carrera");


--
-- Name: Historial_Maquina historial_maquina; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Historial_Maquina"
    ADD CONSTRAINT historial_maquina FOREIGN KEY ("No_maquina") REFERENCES public."Computadora"("No_maquina");


--
-- Name: Historial_Maquina historial_ubicacion; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Historial_Maquina"
    ADD CONSTRAINT historial_ubicacion FOREIGN KEY ("Id_Historial_maquina") REFERENCES public."Ubicacion"("Id_ubicacion");


--
-- Name: Registro registo_ubicacion; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Registro"
    ADD CONSTRAINT registo_ubicacion FOREIGN KEY ("Id_ubicacion") REFERENCES public."Ubicacion"("Id_ubicacion");


--
-- Name: Registro registro_alumno; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Registro"
    ADD CONSTRAINT registro_alumno FOREIGN KEY ("No_Control_alumno") REFERENCES public."Alumno"("No_Control_alumno");


--
-- Name: Ubicacion ubicacion_maquina; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Ubicacion"
    ADD CONSTRAINT ubicacion_maquina FOREIGN KEY ("No_Maquina_ubicacion") REFERENCES public."Computadora"("No_maquina");


--
-- PostgreSQL database dump complete
--

